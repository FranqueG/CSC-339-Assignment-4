using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4.UI
{
    public partial class frmDimensions : Form
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public frmDimensions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nud_Width.Value <= 0 || nud_Height.Value <= 0)
            {
                MessageBox.Show("Invalid values.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
                return;
            }

            this.Width = Convert.ToInt32(nud_Width.Value);
            this.Height = Convert.ToInt32(nud_Height.Value);

        }
    }
}
