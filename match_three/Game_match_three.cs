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
    //2) destroy 3+ ball
    //3) score counter
    //4) start game state without 3+ ball
    //5) Count only close ball
    public class Game_match_three : Control
    {
        int _cellCount = 10;
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

        //Свойства
        public int CellCount
        {
            get
            {
                return _cellCount;
            }
            set
            {
                if (value > 0)
                {
                    _cellCount = value;
                }
            }
        }


        public Game_match_three() : base()
        {
            this.DoubleBuffered = true;
            FillArray();
            Width = 500;
            Height = 500;
            MouseClick += Game_match_three_MouseClick;

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
                    if(c<Colors.Length)
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
                    byte color = (byte)ColorR.Next(1, 5);
                    Point p = new Point(i, j);
                    while(findMatchThree(p, color))
                    {
                        color =(byte) ColorR.Next(1, 5);
                    }
                    CellArray[i, j] = color;
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

                //Выбрана ячейка (Перемещение точки)
                if (selectedCell.X != -1)
                {
                    Point from = new Point(selectedCell.X, selectedCell.Y);
                    Point to = new Point(xp, yp);

                    //Проверка на возможность перемещения шарика из ячейки {from} в ячейку {to}
                    if ((Math.Abs(from.X - to.X) == 1 && from.Y == to.Y ) ||
                        (Math.Abs(from.Y - to.Y) == 1 && from.X == to.X ))
                    {
                        if (CheckMatchThree(from, to))
                        {
                            MessageBox.Show("Больше 3");
                            fillDeletedBalls();
                        }
                    }
                    selectedCell.X = -1;
                    selectedCell.Y = -1;
                    Invalidate();
                }
                //Ячейка не выбрана (выбор точки)
                else
                {
                    Point p = selectedCell;
                    selectedCell.X = xp;
                    selectedCell.Y = yp;
                    Invalidate();
                }
            }
        }

        //Проверить на возможность собрать "три в ряд" и удалить
        public bool CheckMatchThree(Point from, Point to)
        {
            //Для первой фигуры
            List<Point> verticalPoints = new List<Point>();
            List<Point> horizontalPoints = new List<Point>();
            //Для второй фигуры
            List<Point> verticalPoints2 = new List<Point>();
            List<Point> horizontalPoints2 = new List<Point>();
            byte buf = CellArray[to.X, to.Y];
            bool result = false;
            CellArray[to.X, to.Y] = CellArray[from.X, from.Y];
            CellArray[from.X, from.Y] = buf;

            //Поиск три в ряд у первой фигуры
            Point cell = to;
            byte color = CellArray[to.X, to.Y];
            findMatchThree(horizontalPoints, verticalPoints, cell, color);

            if(verticalPoints.Count > 2)
            {
                verticalPoints.ForEach(p => CellArray[p.X, p.Y] = 255);
                result = true;
            }
            if(horizontalPoints.Count > 2)
            {
                horizontalPoints.ForEach(p => CellArray[p.X, p.Y] = 255);
                result = true;
            }

            //Поиск три в ряд у второй фигуры
            cell = from;
            color = CellArray[from.X, from.Y];
            findMatchThree(horizontalPoints2, verticalPoints2, cell, color);
            if (verticalPoints2.Count > 2)
            {
                verticalPoints2.ForEach(p => CellArray[p.X, p.Y] = 255);
                result = true;
            }

            if (horizontalPoints2.Count > 2)
            {
                horizontalPoints2.ForEach(p => CellArray[p.X, p.Y] = 255);
                result = true;
            }

            return result;
        }

        public void fillDeletedBalls()
        {
            Random ColorR = new Random();
            for (int i = 0; i < CellCount; i++)
            {
                for (int j = 0; j < CellCount; j++)
                {
                    if(CellArray[i, j] == 255)
                    {
                        byte color = (byte)ColorR.Next(1, 5);
                        Point p = new Point(i, j);
                        while (findMatchThree(p, color))
                        {
                            color = (byte)ColorR.Next(1, 5);
                        }
                        CellArray[i, j] = color;
                    }
                }
            }
        }

        //Нахождение всех соприкасающихся кругов с кругом в ячейке {cell}, у которых цвет {color}
        public void findMatchThree(List<Point> horizontalPoints, List<Point> verticalPoints, Point cell, byte color)
        {
            horizontalPoints.Add(cell);
            verticalPoints.Add(cell);
            int x = cell.X - 1;
            int y = cell.Y;
            //Влево
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                horizontalPoints.Add(currPoint);

                x--;
            }

            x = cell.X + 1;
            y = cell.Y;
            //Вправо
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                horizontalPoints.Add(currPoint);

                x++;
            }


            x = cell.X;
            y = cell.Y - 1;
            //Вверх
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                verticalPoints.Add(currPoint);

                y--;
            }

            x = cell.X;
            y = cell.Y + 1;
            //Вниз
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                verticalPoints.Add(currPoint);

                y++;
            }
        }

        //Нахождение всех соприкасающихся кругов с кругом в ячейке {cell}, у которых цвет {color} и возвращение были ли они найдены
        public bool findMatchThree(Point cell, byte color)
        {
            List<Point> horizontalPoints = new List<Point>();
            List<Point> verticalPoints = new List<Point>();

            horizontalPoints.Add(cell);
            verticalPoints.Add(cell);

            int x = cell.X - 1;
            int y = cell.Y;
            //Влево
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                horizontalPoints.Add(currPoint);

                x--;
            }

            x = cell.X + 1;
            y = cell.Y;
            //Вправо
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                horizontalPoints.Add(currPoint);

                x++;
            }


            x = cell.X;
            y = cell.Y - 1;
            //Вверх
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                verticalPoints.Add(currPoint);

                y--;
            }

            x = cell.X;
            y = cell.Y + 1;
            //Вниз
            while (isValidCoord(x, y) && CellArray[x, y] == color)
            {
                Point currPoint = new Point(x, y);
                verticalPoints.Add(currPoint);

                y++;
            }

            if(horizontalPoints.Count>2 || verticalPoints.Count > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Координаты не выходят за границы
        public bool isValidCoord(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }
            if (x > CellCount - 1 || y > CellCount - 1)
            {
                return false;
            }

            return true;
        }

    }
}
