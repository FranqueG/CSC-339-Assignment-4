using GameMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4.UI
{
    public partial class Form1 : Form
    {
        Game theGame = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a Connect4 game where you create the size of the board.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (theGame != null)
                    theGame.Board.Dispose();

                this.Close();
            }
        }

        private void newGameToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDimensions form = new frmDimensions();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    theGame = new Game(form.Height, form.Width);

                    theGame.GameStateChanged += TheGame_GameStateChanged;
                    ShowLabelForState();
                    RenderBoard();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void ShowLabelForState()
        {
            switch (theGame.CurrentState)
            {
                case Enums.GameStates.Draw:
                    this.label_Message.Text = "It's a draw!";
                    break;
                case Enums.GameStates.RedsTurn:
                    this.label_Message.Text = "Red, it's your turn";
                    break;
                case Enums.GameStates.RedWins:
                    this.label_Message.Text = "Red wins!";
                    break;
                case Enums.GameStates.YellowsTurn:
                    this.label_Message.Text = "Yellow, it's your turn";
                    break;
                case Enums.GameStates.YellowWins:
                    this.label_Message.Text = "Yellow wins!";
                    break;
            }

        }

        private void TheGame_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            ShowLabelForState();
            RenderBoard();
        }

        /// <summary>
        /// Renders the game board on the UI using a bitmap.
        /// </summary>
        private void RenderBoard()
        {
            if (theGame != null)
            {


                Bitmap bmp = new Bitmap(pb_Render.Width, pb_Render.Height);
                using (var gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(Color.White);

                    // Render the grid

                    var horizontalSpacing = Convert.ToSingle(pb_Render.Width) / Convert.ToSingle(theGame.Board.Width);
                    var verticalSpacing = Convert.ToSingle(pb_Render.Height) / Convert.ToSingle(theGame.Board.Height);

                    float x = 0f;
                    Pen greyPen = new Pen(Color.DarkGray);
                    Pen orangePen = new Pen(Color.Orange);
                    Pen redPen = new Pen(Color.Red);
                    SolidBrush orangeBrush = new SolidBrush(Color.Orange);
                    SolidBrush redBrush = new SolidBrush(Color.Red);
                    // Draw the horizontal and vertical grid lines
                    while (x <= pb_Render.Width + horizontalSpacing)
                    {
                        gfx.DrawLine(greyPen, x, 0, x, pb_Render.Height);
                        x += horizontalSpacing;
                    }
                    float y = 0f;
                    while (y <= pb_Render.Height + verticalSpacing)
                    {
                        gfx.DrawLine(greyPen, 0, y, pb_Render.Width, y);
                        y += verticalSpacing;
                    }

                    // Loop through the discs and draw them
                    for (int xCoordinate = 0; xCoordinate <= theGame.Board.Discs.GetUpperBound(0); xCoordinate++)
                    {
                        for (int yCoordinate = 0; yCoordinate <= theGame.Board.Discs.GetUpperBound(1); yCoordinate++)
                        {
                            Disc currentDisc = theGame.Board.Discs[xCoordinate, yCoordinate];
                            if (currentDisc != null)
                            {
                                if (currentDisc.Side == Enums.Sides.Red)
                                    gfx.FillEllipse(redBrush, new RectangleF(xCoordinate * horizontalSpacing, pb_Render.Height - yCoordinate * verticalSpacing - verticalSpacing, horizontalSpacing, verticalSpacing));
                                else
                                    gfx.FillEllipse(orangeBrush, new RectangleF(xCoordinate * horizontalSpacing, pb_Render.Height - yCoordinate * verticalSpacing - verticalSpacing, horizontalSpacing, verticalSpacing));
                            }

                        }
                    }
                }
                if (this.pb_Render.Image != null)
                {
                    this.pb_Render.Image.Dispose();
                }
                this.pb_Render.Image = bmp;

            }
        }

        private void pb_Render_Click(object sender, EventArgs e)
        {

        }

        private void pb_Render_MouseClick(object sender, MouseEventArgs e)
        {

            // Need to figure out which row it was clicked on based on the coordinates of the mouse click

            if (this.theGame != null && this.theGame.Board!=null)
            {
                var horizontalSpacing = Convert.ToDouble(this.pb_Render.Width) / Convert.ToDouble(theGame.Board.Width);
                int rowIndex = Convert.ToInt32(Math.Floor(Convert.ToDouble(e.X) / horizontalSpacing));

                Disc newDisc = null;
                if (this.theGame.CurrentState == Enums.GameStates.RedsTurn)
                {
                    newDisc = new Disc(Enums.Sides.Red);

                }
                else if (this.theGame.CurrentState == Enums.GameStates.YellowsTurn)
                {
                    newDisc = new Disc(Enums.Sides.Yellow);
                }

                try
                {
                    this.theGame.AddDisc(newDisc, rowIndex);
                }
                catch (Exception ex)
                {
                    this.label_Message.Text = ex.Message;
                }
                RenderBoard();
            }
        }
    }
}
