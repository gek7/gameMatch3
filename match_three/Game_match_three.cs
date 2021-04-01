using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace match_three
{
    //toDo
    //1) move ball
    //2)destroy 3+ ball
    //3)score counter
    //4)start game state without 3+ ball
    public class Game_match_three : Control
    {
        int CellCount = 10;
        Point selectedCell = new Point(-1, -1);
        // Информация о ячейках 
        byte[,] CellArray = new byte[10, 10];
        Brush[] Colors = {
        new SolidBrush(Color.White),
        new SolidBrush(Color.Green),
        new SolidBrush(Color.Red),
        new SolidBrush(Color.Blue),
        new SolidBrush(Color.Yellow)
        };
        public Game_match_three() : base()
        {
            this.DoubleBuffered = true;
            FillArray();
            Width = 500;
            Height = 500;
            MouseClick += Game_match_three_MouseClick; ;
        }

        private void Game_match_three_MouseClick(object sender, MouseEventArgs e)
        {
            SelectBall(e.Y, e.X);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // 18 палок 9 сверха 9 слева 
            // Width-Ширина, Height- Высота
            // Формула для палок сверха в низа: Width делим на колво палок сверха в низа
            // Формула для палок слева в врава: Heigth делим на колво палок слева в врава
            for (int i = 1; i < CellCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), Width / CellCount * i, 0, Width / CellCount * i, Height);
            }
            for (int j = 1; j < CellCount; j++)
            {
                e.Graphics.DrawLine(new Pen(Color.Black), 0, Height / CellCount * j, Width, Height / CellCount * j);
            }
            e.Graphics.DrawLine(new Pen(Color.Black), Width - 1, 0, Width - 1, Height);
            e.Graphics.DrawLine(new Pen(Color.Black), 0, 0, Width, 0);
            e.Graphics.DrawLine(new Pen(Color.Black), 0, 0, 0, Height);
            e.Graphics.DrawLine(new Pen(Color.Black), 0, Height - 1, Width, Height - 1);
            // Рисовать вот так линий
            //e.Graphics.DrawLine(new Pen(Color.Black), 0, 0, Width, Height);
            //Brush brush = new SolidBrush(backColor);
            //e.Graphics.FillRectangle(brush, ClientRectangle);
            int x1, y1, x2, y2;
            y1 = 0;
            y2 = Height / CellCount;
            for (int i = 0; i < CellCount; i++)
            {
                x1 = 0;
                x2 = Width / CellCount;
                for (int j = 0; j < CellCount; j++)
                {
                    byte c = CellArray[i, j];
                    //e.Graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(x1+10, y1+10, Height / RowCount-20, Width / ColCount-20));
                    e.Graphics.FillEllipse(Colors[c], new Rectangle(x1 + 10, y1 + 10, Height / CellCount - 20, Width / CellCount - 20));


                    //Выделение выбранного шара
                    if (CellArray[i, j] != 0)
                    {
                        if (selectedCell.X == i && selectedCell.Y == j)
                            e.Graphics.DrawEllipse(new Pen(Brushes.Black, 2), new Rectangle(x1 + 10, y1 + 10, Height / CellCount - 20, Width / CellCount - 20));
                    }
                    x1 += Width / CellCount;
                    x2 += Width / CellCount;
                }
                y1 += Height / CellCount;
                y2 += Height / CellCount;
            }
        }
        // Заполняет массив
        private void FillArray()
        {
            Random ColorR = new Random();
            for (int i = 0; i < CellCount; i++)
            {
                for (int j = 0; j < CellCount; j++)
                {
                    CellArray[i, j] =(byte) ColorR.Next(1, 5);
                }
            }
        }

        //Выбор или перемещение шара
        public virtual void SelectBall(int x, int y)
        {
            //Смещение для правильного нахождения ячейки
            int offset = (CellCount / 2);
            //x и y кликнутой ячейки
            int xp = x / ((Width - offset) / CellCount);
            int yp = y / ((Height - offset) / CellCount);
            if (xp < CellCount && yp < CellCount)
            {
                //Значение выбраной ячейки
                int value = CellArray[xp, yp];

                //Ячейка пустая (Перемещение точки)
                if (value == 0)
                {
                    if (selectedCell.X != -1)
                    {
                       
                    }
                }
                //Ячейка не пустая (перевыбор точки)
                else
                {
                    Point p = selectedCell;
                    selectedCell.X = xp;
                    selectedCell.Y = yp;
                    Invalidate();
                }
            }
        }

        //Удаляет 3+ в ряд
        public bool removeStackIfExists(Point cell)
        {
            List<Point> lst = new List<Point>();
            List<Point> buf = new List<Point>();
            int index = 0;
            lst.Add(cell);
            while (index<lst.Count)
            {
                Point p = lst.Last();
                //if (p.)
                //{

                //}
                index++;
            }

            //////Удаление//////
            while (lst.Count > 0)
            {
                Point p = lst.Last();
                //CellArray[p.X, p.Y] = null;
                lst.RemoveAt(lst.Count - 1);
                //score++;
            }
            return false;
        }

    }
}
