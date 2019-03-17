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

            for (int i = 0; i <= Rows; i++)
            {
                for (int j = 0; j <= Columns; j++)
                {
                    Console.ForegroundColor = mas[i, j].Color;
                    Console.SetCursorPosition(j, i);
                    Console.Write(mas[i, j].Symbol);
                }
            }

        }
    }
}
