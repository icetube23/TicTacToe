using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        Game game;
        bool computer = false;
        int msgIndex = 0;
        string[] defeatMsg = new string[6] { "Do you wish to give up?",
                                             "Do you accept defeat?",
                                             "Do you realize the fatalism of your actions?",
                                             "Your efforts pointless.",
                                             "You have no meaning.",
                                             "You can never win." };

        public TicTacToe()
        {
            InitializeComponent();
        }
        
        private void GameStep()
        {
            RenderGame();
            if (game.Finished())
            {
                string message = game.Result();
                if (computer)
                {
                    if (message != "Draw!") { message = "Computer wins!"; }
                    message += "\n" + defeatMsg[msgIndex++];
                    msgIndex %= defeatMsg.Length;
                }
                else
                {
                    message += "\nDo you want to play again?";
                }
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                result = MessageBox.Show(message, computer ? "Windows" : "Game ended", buttons);

                if ((result == System.Windows.Forms.DialogResult.Yes && !computer)
                    || (result == System.Windows.Forms.DialogResult.No && computer))
                {
                    game = computer ? new Player() : new Game();
                    RenderGame();
                }
                else
                {
                    Close();
                }
            }
        }
        private void RenderGame()
        {
            label1.Text = game.Field(0, 0).ToString();
            label2.Text = game.Field(0, 1).ToString();
            label3.Text = game.Field(0, 2).ToString();
            label4.Text = game.Field(1, 0).ToString();
            label5.Text = game.Field(1, 1).ToString();
            label6.Text = game.Field(1, 2).ToString();
            label7.Text = game.Field(2, 0).ToString();
            label8.Text = game.Field(2, 1).ToString();
            label9.Text = game.Field(2, 2).ToString();
        }

        private void TicTacToe_Load(object sender, EventArgs e)
        {
            string message = "Do you want to play against the computer?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, "TicTacToe", buttons);

            game = result == System.Windows.Forms.DialogResult.Yes ? new Player() : new Game();
            computer = result == System.Windows.Forms.DialogResult.Yes ? true : false;

            RenderGame();
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            game.MakeTurn(0, 0);
            GameStep();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            game.MakeTurn(0, 1);
            GameStep();
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            game.MakeTurn(0, 2);
            GameStep();
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            game.MakeTurn(1, 0);
            GameStep();
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            game.MakeTurn(1, 1);
            GameStep();
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            game.MakeTurn(1, 2);
            GameStep();
        }

        private void Label7_Click(object sender, EventArgs e)
        {
            game.MakeTurn(2, 0);
            GameStep();
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            game.MakeTurn(2, 1);
            GameStep();
        }

        private void Label9_Click(object sender, EventArgs e)
        {
            game.MakeTurn(2, 2);
            GameStep();
        }
    }
}
