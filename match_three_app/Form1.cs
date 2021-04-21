using match_three;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace match_three_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            game_match_three1.scoreChanged += Game_match_three1_scoreChanged;
        }

        private void Game_match_three1_scoreChanged()
        {
            label1.Text = "Счёт: " + game_match_three1.Score;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game_match_three1.NewGame();
        }
    }
}
