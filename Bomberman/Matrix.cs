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

        public int GenerateRandomMatrix(int size)
        {
            Space space = new Space();
            Player player = new Player();
            Brick brick = new Brick();
            Concrete concrete = new Concrete();
            Random random = new Random();
            int bricks = 0;
            mas = new Elem[size, size];

            mas[0, 0] = player;
            mas[0, 1] = space;
            mas[1, 0] = space;

            int randomInt;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if(i+j != 0 && i+j != 1)
                    {
                        randomInt = random.Next(0, 4);

                        switch(randomInt)
                        {
                            case 0:
                                mas[i, j] = brick;
                                bricks++;
                                break;
                            case 1:
                                mas[i, j] = brick;
                                bricks++;
                                break;
                            case 2:
                                mas[i, j] = space;
                                break;
                            case 3:
                                mas[i, j] = concrete;
                                break;
                        }
                    }
                }
            }
            return bricks;
            
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
