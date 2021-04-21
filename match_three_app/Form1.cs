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
            textBox1.Text = game_match_three1.MaxScore.ToString();
            initColors();
        }

        private void Game_match_three1_scoreChanged()
        {
            label1.Text = "Счёт: " + game_match_three1.Score;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game_match_three1.NewGame();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            game_match_three1.CvetFiguri1 = new SolidBrush(cd.Color);
            initColors();
        }

        public void initColors()
        {
            pictureBox1.BackColor = game_match_three1.CvetFiguri1.Color;
            pictureBox2.BackColor = game_match_three1.CvetFiguri2.Color;
            pictureBox3.BackColor = game_match_three1.CvetFiguri3.Color;
            pictureBox4.BackColor = game_match_three1.CvetFiguri4.Color;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            game_match_three1.CvetFiguri2 = new SolidBrush(cd.Color);
            initColors();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            game_match_three1.CvetFiguri3 = new SolidBrush(cd.Color);
            initColors();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            game_match_three1.CvetFiguri4 = new SolidBrush(cd.Color);
            initColors();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int result = Int32.Parse(textBox1.Text);
                game_match_three1.MaxScore = result;
                MessageBox.Show("Максимальный счёт изменён","Успех",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Некорректное значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
