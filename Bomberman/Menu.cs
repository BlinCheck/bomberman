using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Menu
    {
        private bool NewMenu = true;

        public void DisplayMenu()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(5, 0);
            Console.Write("BOMBERMAN");
            Console.SetCursorPosition(6, 1);
            Console.WriteLine("Menu");
            Console.WriteLine(" Play");
            Console.WriteLine(" Manual");
            Console.WriteLine(" Quit");
            MenuSelector();
        }

        private void DisplayManual()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Welcome to Bomberman!");
            Console.WriteLine(" This is short manual for the game:");
            Console.WriteLine(" Use W, A, S, D to move");
            Console.WriteLine(" Use E to set the bomb");
            Console.WriteLine(" During game you can pick up different bonuses:");
            Console.WriteLine(" * - Additioal bomb");
            Console.WriteLine(" L - Additional life");
            Console.WriteLine(" T - Additional time");
            Console.WriteLine(" % - doubles your current score");
            Console.WriteLine(" + - artifact that gives you 200 points");
            Console.WriteLine(" Win condition: destroy all bricks(#) to win");
            Console.WriteLine(" If you run out of bombs, time or lives - you lose");
            Console.WriteLine(" Your final score depends on time and amount of bombs and lives left");
            Console.WriteLine(" Good luck!");
            Console.WriteLine(" Press any key to return to menu");
            Console.ReadKey(true);
            DisplayMenu();
        }

        private void MenuSelector()
        {
            NewMenu = true;
            int row = 2;
            Console.SetCursorPosition(0, row);
            Console.Write(">");
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (NewMenu)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (row > 2)
                        {
                            Console.SetCursorPosition(0, row--);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row < 4)
                        {
                            Console.SetCursorPosition(0, row++);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.Enter:
                        ChooseOption(row);
                        NewMenu = false;
                        break;
                }
            }
        }

        private void ChooseOption(int row)
        {
            switch(row)
            {
                case 2:
                    DisplayLevels();
                    break;
                case 3:
                    DisplayManual();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }

        private void DisplayLevels()
        {
            Console.Clear();
            Console.SetCursorPosition(3, 0);
            Console.WriteLine("Choose level:");
            Console.WriteLine(" Level 1");
            Console.WriteLine(" Level 2");
            Console.WriteLine(" Level 3");
            Console.WriteLine(" Back");
            LevelSelector();
        }

        private void LevelSelector()
        {
            NewMenu = true;
            int row = 1;
            Console.SetCursorPosition(0, row);
            Console.Write(">");
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (NewMenu)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (row > 1)
                        {
                            Console.SetCursorPosition(0, row--);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row < 4)
                        {
                            Console.SetCursorPosition(0, row++);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.Enter:
                        ChooseLevel(row);
                        NewMenu = false;
                        break;
                }
            }
        }

        private void ChooseLevel(int row)
        {
            string json = "";

            switch (row)
            {
                case 1:
                    json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level1.json");
                    break;
                case 2:
                    json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level2.json");
                    break;
                case 3:
                    json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level3.json");
                    break;
                case 4:
                    DisplayMenu();
                    break;
            }

            Level.Deserializer(json);
        }
    }
}
