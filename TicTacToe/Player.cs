using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Player : Game
    {
        bool firstTurn = false;
        bool easy = false;

        public Player()
        {
            if (0.5 < new Random().NextDouble())
            {
                field[1, 1] = 'O';
                countO++;
                firstTurn = true;
            }
        }

        public override void MakeTurn(int i, int j)
        {
            base.MakeTurn(i, j);
            if (!finished) { ComputerTurn(); }
        }

        private void ComputerTurn()
        {
            if (field[1, 1] == ' ') { base.MakeTurn(1, 1); return; }

            Tuple<int, int> move = PossibleVictory('O');
            if (move is null) { move = PossibleVictory('X'); }
            if (move is null) { move = easy ? RandomDraw() : BestMove(); }
            if (field[move.Item1, move.Item2] != ' ')
            {
                Console.WriteLine("CountX: " + countX + "\nCountO: " + countO + "\ni: " + move.Item1 + " j: " + move.Item2);
            }
            base.MakeTurn(move.Item1, move.Item2);
        }

        private Tuple<int, int> PossibleVictory(char symbol)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                if (field[i, 0] == ' ') 
                {
                    if (field[i, 1] == field[i, 2] && field[i, 1] == symbol)
                    {
                        return new Tuple<int, int>(i, 0);
                    }
                }
                else if (field[i, 1] == ' ')
                {
                    if (field[i, 0] == field[i, 2] && field[i, 0] == symbol)
                    {
                        return new Tuple<int, int>(i, 1);
                    }
                }
                else if (field[i, 2] == ' ')
                {
                    if (field[i, 0] == field[i, 1] && field[i, 0] == symbol)
                    {
                        return new Tuple<int, int>(i, 2);
                    }
                }

                if (field[0, i] == ' ')
                {
                    if (field[1, i] == field[2, i] && field[1, i] == symbol)
                    {
                        return new Tuple<int, int>(0, i);
                    }
                }
                else if (field[1, i] == ' ')
                {
                    if (field[0, i] == field[2, i] && field[0, i] == symbol)
                    {
                        return new Tuple<int, int>(1, i);
                    }
                }
                else if (field[2, i] == ' ')
                {
                    if (field[0, i] == field[1, i] && field[0, i] == symbol)
                    {
                        return new Tuple<int, int>(2, i);
                    }
                }
            }

            if (field[1, 1] == symbol)
            {
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (field[i, j] == ' ' && field[2 - i, 2 - j] == symbol)
                        {
                            return new Tuple<int, int>(i, j);
                        }
                    }
                }
            }
            return null;
        }

        private Tuple<int, int> BestMove()
        {

            Random random = new Random();
            if (firstTurn)
            {
                switch (countO)
                {
                    case 1:
                        if (field[0, 0] == 'X')
                        {
                            return new Tuple<int, int>(2, 2);
                        }
                        else if (field[0, 2] == 'X')
                        {
                            return new Tuple<int, int>(2, 0);
                        }
                        else if (field[2, 0] == 'X')
                        {
                            return new Tuple<int, int>(0, 2);
                        }
                        else if (field[2, 2] == 'X')
                        {
                            return new Tuple<int, int>(0, 0);
                        }
                        else
                        {
                            return new Tuple<int, int>(random.Next(2) * 2, random.Next(2) * 2);
                        }
                    case 2:
                        if (field[0, 0] == 'O')
                        {
                            return field[0, 1] == 'X' ? new Tuple<int, int>(1, 0) : new Tuple<int, int>(0, 1);
                        }
                        else if (field[0, 2] == 'O')
                        {
                            return field[0, 1] == 'X' ? new Tuple<int, int>(1, 2) : new Tuple<int, int>(0, 1);
                        }
                        else if (field[2, 0] == 'O')
                        {
                            return field[1, 0] == 'X' ? new Tuple<int, int>(2, 1) : new Tuple<int, int>(1, 0);
                        }
                        else if (field[2, 2] == 'O')
                        {
                            return field[1, 2] == 'X' ? new Tuple<int, int>(2, 1) : new Tuple<int, int>(1, 2);
                        }
                        break;
                    case 3:
                        if (field[0, 1] == ' ')
                        {
                            return new Tuple<int, int>(random.Next(2) * 2, 1);
                        }
                        else if (field[1, 0] == ' ')
                        {
                            return new Tuple<int, int>(1, random.Next(2) * 2);
                        }
                        break;
                    case 4:
                        for (int i = 0; i < field.GetLength(0); i++)
                        {
                            for (int j = 0; j < field.GetLength(1); j++)
                            {
                                if (field[i, j] == ' ') { return new Tuple<int, int>(i, j); }
                            }
                        }
                        break;
                }
            }
            else
            {
                switch (countO)
                {
                    case 0:
                        return new Tuple<int, int>(random.Next(2) * 2, random.Next(2) * 2);
                    case 1:
                        if (field[1, 1] == 'X')
                        {
                            int i = random.Next(2) * 2;
                            if (field[0, 0] == 'O' || field[2, 2] == 'O')
                            {
                                return new Tuple<int, int>(i, 2 - i);
                            }
                            else if (field[0, 2] == 'O' || field[2, 0] == 'O')
                            {
                                return new Tuple<int, int>(i, i);
                            }
                        }
                        else
                        {
                            if (field[0, 1] == 'X' && (field[1, 0] == 'X' || field[1, 2] == 'X'))
                            {
                                return field[1, 0] == 'X' ? new Tuple<int, int>(0, 0) : new Tuple<int, int>(0, 2);
                            }
                            else if (field[2, 1] == 'X' && (field[1, 0] == 'X' || field[1, 2] == 'X'))
                            {
                                return field[1, 0] == 'X' ? new Tuple<int, int>(2, 0) : new Tuple<int, int>(2, 2);
                            }
                            else
                            {
                                return RandomDrawOpposite();
                            }
                        }
                        break;
                    case 2:
                        if (field[1, 1] == 'O')
                        {
                            return RandomDrawOpposite();
                        }
                        else
                        {
                            return RandomDraw();
                        }
                    case 3:
                        return RandomDraw();
                }
            }
            return null;
        }

        private Tuple<int, int> RandomDraw()
        {
            Random random = new Random();
            int i, j;
            do
            {
                i = random.Next(3);
                j = random.Next(3);
            } while (field[i, j] != ' ');
            return new Tuple<int, int>(i, j);
        }
        private Tuple<int, int> RandomDrawOpposite()
        {
            Tuple<int, int> result;
            do
            {
                result = RandomDraw();
            } while (field[2 - result.Item1, 2 - result.Item2] == 'O');
            return result;
        }
    }
}
