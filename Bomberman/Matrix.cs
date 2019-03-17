using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Matrix
    {
        public Elem[,] mas;
        public int Rows {get; set;}
        public int Columns { get; set; }

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
            Space space = new Space();
            Player player = new Player();
            Brick brick = new Brick();
            Concrete concrete = new Concrete();
            mas = new Elem[,] { 
                {player, space, space, brick, space, space, brick, brick, space, brick},
                {space, concrete, brick, concrete, space, concrete, space, concrete, space, concrete},
                {brick, brick, space, brick, brick, space, brick, brick, brick, brick},
                {space, concrete, brick, concrete, space, concrete, space, concrete, brick, concrete},
                {space, brick, brick, brick, space, space, brick, brick, brick, space}
            };
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
