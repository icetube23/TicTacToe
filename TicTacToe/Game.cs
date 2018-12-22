using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Game
    {
        protected char[,] field = new char[3, 3];
        protected char playerSymbol = 'X';
        protected bool finished = false;
        protected int countX = 0;
        protected int countO = 0;

        public Game()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = ' ';
                }
            }
        }

        public char Field(int i, int j)
        {
            return field[i, j];
        }
        public bool Finished()
        {
            return finished;
        }

        public virtual void MakeTurn(int i, int j)
        {
            if (field[i, j] != ' ' || finished) { return; }
            field[i, j] = playerSymbol;
            if (playerSymbol == 'X') { countX++; }
            else { countO++; }
            CheckForEnd();
            if (!finished) { playerSymbol = playerSymbol == 'X' ? 'O' : 'X'; }
        }

        private void CheckForEnd()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                if (field[i, 0] != ' ' && field[i, 0] == field[i, 1] && field[i, 1] == field[i, 2])
                {
                    finished = true;
                    return;
                }

                if (field[0, i] != ' ' && field[0, i] == field[1, i] && field[1, i] == field[2, i])
                {
                    finished = true;
                    return;
                }
            }

            if (field[0, 0] != ' ' && field[0, 0] == field[1, 1] && field[1, 1] == field[2, 2])
            {
                finished = true;
                return;
            }

            if (field[0, 2] != ' ' && field[0, 2] == field[1, 1] && field[1, 1] == field[2, 0])
            {
                finished = true;
                return;
            }

            bool noMovesLeft = true;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == ' ') { noMovesLeft = false; }
                }
            }
            if (noMovesLeft) { finished = true; playerSymbol = ' '; }
        }

        public string Result()
        {
            if (!finished) { return "On going..."; }
            if (playerSymbol == ' ') { return "Draw!"; }
            return "Player '" + playerSymbol + "' wins!";
        }
    }
}
