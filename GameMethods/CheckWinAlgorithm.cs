using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMethods
{
    /// <summary>
    /// Interface for defining an algorithm for checking for a winning condition.
    /// </summary>
    public interface ICheckWinAlgorithm
    {
        bool CheckForWinningCondition(Board board,Disc disc);
    }

    /// <summary>
    /// Performs a horizontal winning condition check.
    /// </summary>
    public class HorizontalWinCheckAlgorithm : ICheckWinAlgorithm
    {
        public bool CheckForWinningCondition(Board board, Disc disc)
        {
            int countOfSameColor = 0; // Number of discs with the same color

            // Check forward. Can not go greater than the width of the board
            for (int x = disc.XCoordinate.Value; x <= Math.Min((disc.XCoordinate.Value + Game.cConnectionsRequired), board.Discs.GetUpperBound(0)); x++)
            {

                if (board.Discs[x, disc.YCoordinate.Value] != null && board.Discs[x, disc.YCoordinate.Value].Side == disc.Side)
                    countOfSameColor++;
                else

                    break;
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true;

            countOfSameColor = 0; 

            // Check backward. Can not go lower than 0.
            for (int x = disc.XCoordinate.Value; x >= Math.Max(disc.XCoordinate.Value - Game.cConnectionsRequired, 0); x--)
            {

                if (board.Discs[x, disc.YCoordinate.Value] != null && board.Discs[x, disc.YCoordinate.Value].Side == disc.Side)
                    countOfSameColor++;
                else

                    break;
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true;
            else
                return false;

        }
    }

    /// <summary>
    /// Performs vertical checks for winning conditions.
    /// </summary>
    public class VerticalWinCheckAlgorithm : ICheckWinAlgorithm
    {
        public bool CheckForWinningCondition(Board board, Disc disc)
        {
            int countOfSameColor = 0; 

            // Check upwards. Can not go higher than the height of the board.
            for (int y = disc.YCoordinate.Value; y <= Math.Min((disc.YCoordinate.Value + Game.cConnectionsRequired), board.Discs.GetUpperBound(1)); y++)
            {

                if (board.Discs[disc.XCoordinate.Value, y] != null && board.Discs[disc.XCoordinate.Value, y].Side == disc.Side)
                    countOfSameColor++;
                else

                    break;
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true; 

            countOfSameColor = 0; 

            // Check downward. Can not go below 0.
            for (int y = disc.YCoordinate.Value; y >= Math.Max(disc.YCoordinate.Value - Game.cConnectionsRequired, 0); y--)
            {

                if (board.Discs[disc.XCoordinate.Value, y] != null && board.Discs[disc.XCoordinate.Value, y].Side == disc.Side)
                    countOfSameColor++;
                else

                    break;
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// Performs diagonal checks for winning conditions.
    /// </summary>
    public class DiagonalWinCheckAlgorithm : ICheckWinAlgorithm
    {
        public bool CheckForWinningCondition(Board board, Disc disc)
        {
            int countOfSameColor = 0; // Number of discs with the same colour

            // Checks for 4 diagonal win conditions

            // 1) positive X, positive Y first - checking for "up and right" 
            // We are incrementing both X and Y axis values
            for (int i = 0; i < Game.cConnectionsRequired; i++)
            {
                if (disc.XCoordinate.Value + i <= board.Discs.GetUpperBound(0) && disc.YCoordinate.Value + i <= board.Discs.GetUpperBound(1))
                {
                    if (board.Discs[disc.XCoordinate.Value + i, disc.YCoordinate.Value + i] != null && board.Discs[disc.XCoordinate.Value + i, disc.YCoordinate.Value + i].Side == disc.Side)
                        countOfSameColor++;
                    else
                        break; 
                }
                else
                    break; 
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true; 

            countOfSameColor = 0; 

            // 2) positive X, negative Y - this is checking "down and right"
            // We are incrementing X axis values but decrementing Y axis values

            for (int i = 0; i < Game.cConnectionsRequired; i++)
            {
                if (disc.XCoordinate.Value + i <= board.Discs.GetUpperBound(0) && disc.YCoordinate.Value - i >= 0)
                {
                    if (board.Discs[disc.XCoordinate.Value + i, disc.YCoordinate.Value - i] != null && board.Discs[disc.XCoordinate.Value + i, disc.YCoordinate.Value - i].Side == disc.Side)
                        countOfSameColor++;
                    else
                        break; 
                }
                else
                    break; 
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true;

            countOfSameColor = 0;

            // 3) negative X, positive Y - this is checking "up and left" 
            // Decrement X axis values but increment Y axis values

            for (int i = 0; i < Game.cConnectionsRequired; i++)
            {
                if (disc.XCoordinate.Value - i >= 0 && disc.YCoordinate.Value + i <= board.Discs.GetUpperBound(1))
                {
                    if (board.Discs[disc.XCoordinate.Value - i, disc.YCoordinate.Value + i] != null && board.Discs[disc.XCoordinate.Value - i, disc.YCoordinate.Value + i].Side == disc.Side)
                        countOfSameColor++;
                    else
                        break; 
                }
                else
                    break; 
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true; 

            countOfSameColor = 0;

            // 4) negative X, negative Y - this is checking "down and left" 
            // Decrement both X and Y axis values
            for (int i = 0; i < Game.cConnectionsRequired; i++)
            {
                if (disc.XCoordinate.Value - i >= 0 && disc.YCoordinate.Value - i >= 0)
                {
                    if (board.Discs[disc.XCoordinate.Value - i, disc.YCoordinate.Value - i] != null && board.Discs[disc.XCoordinate.Value - i, disc.YCoordinate.Value - i].Side == disc.Side)
                        countOfSameColor++;
                    else
                        break; 
                }
                else
                    break; 
            }

            if (countOfSameColor >= Game.cConnectionsRequired)
                return true; 
            else
                return false;
        }
    }
}
