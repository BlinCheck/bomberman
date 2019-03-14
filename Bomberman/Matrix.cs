using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Matrix
    {
        Elem[,] mas;

        public Elem this[int i, int j]
        {
            get
            {
                return mas[i, j];
            }
            set
            {
                mas[i, j] = value;
            }
        }

        public void GenerateMatrix()
        {
            mas = new Elem[,] { {new Player(), new Space(), new Space(), new Brick(), new Space(), new Space(),
                new Brick(), new Brick(), new Space(), new Brick()},
                {new Space(), new Concrete(), new Brick(), new Concrete(), new Space(), new Concrete(), new Space(),
                new Concrete(), new Space(), new Concrete()},
                {new Brick(), new Brick(), new Space(), new Brick(), new Brick(), new Space(), new Brick(),
                new Brick(), new Brick(), new Brick()},
                {new Space(), new Concrete(), new Brick(), new Concrete(), new Space(), new Concrete(), new Space(),
                new Concrete(), new Brick(), new Concrete()},
                {new Space(), new Brick(), new Brick(), new Brick(), new Space(), new Space(), new Brick(),
                new Brick(), new Brick(), new Space()} };
        }

        public void DisplayMatrix()
        {
            int rows = mas.GetUpperBound(0) + 1;
            int columns = mas.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(mas[i, j].Symbol);
                }
            }

        }
    }
}
