﻿using System;
using System.Threading;

namespace Bomberman
{
    class Program
    {
        static int playerX = 0;
        static int playerY = 0;
        static bool bombOn = false;
        static int bombX;
        static int bombY;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Session.isAlive = true;
            Matrix matrix = new Matrix();
            matrix.generateMatrix();
            matrix.displayMatrix();
            Time time = new Time(0, 30);

            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (Session.isAlive)
            {
                key = Console.ReadKey(true);
                if (Session.isAlive)
                {
                    if (matrix[playerX, playerY].Name.Equals("bomb"))
                        moveFromBomb(matrix, key);
                    else
                    {
                        switch (key.Key)
                        {
                            case ConsoleKey.Q:
                                break;
                            case ConsoleKey.W:
                                if (playerX - 1 >= 0 && matrix[playerX - 1, playerY].Name.Equals("space"))
                                {
                                    matrix[playerX, playerY] = new Elem(' ', "space", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write(' ');
                                    playerX -= 1;
                                    matrix[playerX, playerY] = new Elem('I', "player", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write('I');
                                }
                                break;
                            case ConsoleKey.D:
                                if (playerY + 1 <= 9 && matrix[playerX, playerY + 1].Name.Equals("space"))
                                {
                                    matrix[playerX, playerY] = new Elem(' ', "space", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write(' ');
                                    playerY += 1;
                                    matrix[playerX, playerY] = new Elem('I', "player", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write('I');

                                }
                                break;
                            case ConsoleKey.A:
                                if (playerY - 1 >= 0 && matrix[playerX, playerY - 1].Name.Equals("space"))
                                {
                                    matrix[playerX, playerY] = new Elem(' ', "space", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write(' ');
                                    playerY -= 1;
                                    matrix[playerX, playerY] = new Elem('I', "player", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write('I');

                                }
                                break;
                            case ConsoleKey.S:
                                if (playerX + 1 <= 4 && matrix[playerX + 1, playerY].Name.Equals("space"))
                                {
                                    matrix[playerX, playerY] = new Elem(' ', "space", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write(' ');
                                    playerX += 1;
                                    matrix[playerX, playerY] = new Elem('I', "player", true);
                                    Console.SetCursorPosition(playerY, playerX);
                                    Console.Write('I');
                                }
                                break;
                            case ConsoleKey.E:
                                if (bombOn == false)
                                    setBomb(matrix);
                                break;
                        }
                    }
                }
            }

            while (key.Key != ConsoleKey.Q)
                key = Console.ReadKey(true);
            
        }

        public static void timer(object obj)
        {
            if(Session.isAlive)
            {
                Time time = (Time)obj;

                Console.SetCursorPosition(13, 0);
                if (time.Minutes < 10)
                    Console.Write($"Time: 0{time.Minutes}:");
                else
                    Console.Write($"Time: {time.Minutes}:");

                if (time.Seconds < 10)
                    Console.Write($"0{time.Seconds}");
                else
                    Console.Write($"{time.Seconds}");

                if (time.Seconds == 0)
                {
                    if (time.Minutes == 0)
                        Session.End();
                    else
                    {
                        time.Minutes--;
                        time.Seconds = 59;
                    }
                }
                else
                    time.Seconds--;
            }
        }

        public static void setBomb(Matrix matrix)
        {
            Console.SetCursorPosition(playerY, playerX);
            Console.Write('@');
            matrix[playerX, playerY] = new Elem('@', "bomb", true);
            bombOn = true;
            bombX = playerX;
            bombY = playerY;
            TimerCallback timerCB = new TimerCallback(explodeBomb);
            Timer t = new Timer(timerCB, matrix, 1500, -1);
        }

        public static void explodeBomb(object obj)
        {
            Matrix matrix = (Matrix)obj;
            bool playerIsFound = false;
            if(bombX-1 >= 0 && matrix[bombX-1, bombY].Destroyable == true)
            {
                matrix[bombX - 1, bombY] = new Elem('.', "ruine", false);
                Console.SetCursorPosition(bombY, bombX - 1);
                Console.Write('.');
            }

            if (bombX + 1 <= 4 && matrix[bombX+1, bombY].Destroyable == true)
            {
                matrix[bombX + 1, bombY] = new Elem('.', "ruine", false);
                Console.SetCursorPosition(bombY, bombX + 1);
                Console.Write('.');
            }

            if (bombY - 1 >= 0 && matrix[bombX, bombY-1].Destroyable == true)
            {
                matrix[bombX, bombY-1] = new Elem('.', "ruine", false);
                Console.SetCursorPosition(bombY-1, bombX);
                Console.Write('.');
            }

            if (bombY + 1 <= 9 && matrix[bombX, bombY+1].Destroyable == true)
            {
                matrix[bombX, bombY+1] = new Elem('.', "ruine", false);
                Console.SetCursorPosition(bombY+1, bombX);
                Console.Write('.');
            }

            matrix[bombX, bombY] = new Elem('.', "ruine", false);
            Console.SetCursorPosition(bombY, bombX);
            Console.Write('.');
            bombOn = false;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (matrix[i, j].Name.Equals("player"))
                    {
                        playerIsFound = true;
                        break;
                    }
                    if (playerIsFound)
                        break;
                }
            }

            Timer tm = new Timer(clearRuines, matrix, 800, -1);

            if (!playerIsFound)
                Session.End();
        }

        public static void clearRuines(object obj)
        {
            Matrix matrix = (Matrix)obj;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (matrix[i, j].Name.Equals("ruine"))
                    {
                        matrix[i, j] = new Elem(' ', "space", true);
                        Console.SetCursorPosition(j, i);
                        Console.Write(' ');
                    }
                }
            }
        }

        public static void moveFromBomb(Matrix matrix, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.W:
                    if (playerX - 1 >= 0 && matrix[playerX - 1, playerY].Name.Equals("space"))
                    {

                        playerX -= 1;
                        matrix[playerX, playerY] = new Elem('I', "player", true);
                        Console.SetCursorPosition(playerY, playerX);
                        Console.Write('I');
                    }
                    break;
                case ConsoleKey.D:
                    if (playerY + 1 <= 9 && matrix[playerX, playerY + 1].Name.Equals("space"))
                    {

                        playerY += 1;
                        matrix[playerX, playerY] = new Elem('I', "player", true);
                        Console.SetCursorPosition(playerY, playerX);
                        Console.Write('I');

                    }
                    break;
                case ConsoleKey.A:
                    if (playerY - 1 >= 0 && matrix[playerX, playerY - 1].Name.Equals("space"))
                    {

                        playerY -= 1;
                        matrix[playerX, playerY] = new Elem('I', "player", true);
                        Console.SetCursorPosition(playerY, playerX);
                        Console.Write('I');

                    }
                    break;
                case ConsoleKey.S:
                    if (playerX + 1 <= 4 && matrix[playerX + 1, playerY].Name.Equals("space"))
                    {

                        playerX += 1;
                        matrix[playerX, playerY] = new Elem('I', "player", true);
                        Console.SetCursorPosition(playerY, playerX);
                        Console.Write('I');
                    }
                    break;
            }
        }
    }

    class Time
    {
        private int minutes;
        private int seconds;
        private Timer t;

        public Time(int minutes, int seconds)
        {
            this.Minutes = minutes;
            this.Seconds = seconds;
            t = new Timer(Program.timer, this, 0, 1000);
        }

        public int Minutes
        {
            get
            {
                return this.minutes;
            }

            set
            {
                if (value >= 0 && value < 60)
                    this.minutes = value;
            }
        }

        public int Seconds
        {
            get
            {
                return this.seconds;
            }

            set
            {
                if (value >= 0 && value < 60)
                    this.seconds = value;
            }
        }
    }

    struct Session
    {
        public static bool isAlive;

        public static void End()
        {
            Session.isAlive = false;
            Console.SetCursorPosition(0, 6);
            Console.WriteLine("Game Over");
            Console.Write("Press \"Q\" to quit");
        }
    }


    struct Elem
    {
        public char Symbol { get; set; }
        public string Name { get; set; }
        public bool Destroyable { get; set; }


        public Elem(char sym, string name, bool destroy)
        {
            this.Symbol = sym;
            this.Name = name;
            this.Destroyable = destroy;
        }
    }

    struct Matrix
    {
        Elem[,] mas;
        public Elem this[int i, int j]
        {
            get
            {
                return mas[i,j];
            }
            set
            {
                mas[i, j] = value;
            }
        }

        public void generateMatrix()
        {
            mas = new Elem[,] { {new Elem('I', "player", true), new Elem(' ', "space", true), new Elem(' ', "space", true),
                                 new Elem('#', "brick", true), new Elem(' ', "space", true), new Elem(' ', "space", true),
                                 new Elem('#', "brick", true), new Elem('#', "brick", true), new Elem(' ', "space", true),
                                 new Elem('#', "brick", true)}, 
                {new Elem(' ', "space", true), new Elem('o', "concrete", false), new Elem('#', "brick", true),
                 new Elem('o', "concrete", false), new Elem(' ', "space", true), new Elem('o', "concrete", false),
                 new Elem(' ', "space", true), new Elem('o', "concrete", false), new Elem(' ', "space", true),
                 new Elem('o', "concrete", false)},
                {new Elem('#', "brick", true), new Elem('#', "brick", true), new Elem(' ', "space", true),
                 new Elem('#', "brick", true), new Elem('#', "brick", true), new Elem(' ', "space", true),
                 new Elem('#', "brick", true), new Elem('#', "brick", true), new Elem('#', "brick", true),
                 new Elem('#', "brick", true)}, 
                {new Elem(' ', "space", true), new Elem('o', "concrete", false), new Elem('#', "brick", true),
                 new Elem('o', "concrete", false), new Elem(' ', "space", true), new Elem('o', "concrete", false),
                 new Elem(' ', "space", true), new Elem('o', "concrete", false), new Elem('#', "brick", true),
                 new Elem('o', "concrete", false)}, 
                {new Elem(' ', "space", true), new Elem('#', "brick", true), new Elem('#', "brick", true),
                 new Elem('#', "brick", true), new Elem(' ', "space", true), new Elem(' ', "space", true),
                 new Elem('#', "brick", true), new Elem('#', "brick", true), new Elem('#', "brick", true),
                 new Elem(' ', "space", true)} };
        }

        public void displayMatrix()
        {
            int rows = mas.GetUpperBound(0) + 1;
            int columns = mas.Length / rows;

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(mas[i,j].Symbol);
                }
            }
            
        }

       
    }

    
}
