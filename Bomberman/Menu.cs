using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Menu
    {
        const int MENU_SELECTOR_UPPER = 2;
        const int MENU_SELECTOR_BOTTOM = 4;
        const int LEVEL_SELECTOR_UPPER = 1;
        const int LEVEL_SELECTOR_BOTTOM = 7;
        const int MATRIX_SELECTOR_UPPER = 1;
        const int MATRIX_SELECTOR_BOTTOM = 5;

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
            Console.WriteLine(" E: set the bomb");
            Console.WriteLine(" B: save progress");
            Console.WriteLine(" Q: return to menu");
            Console.WriteLine(" During game you can pick up different bonuses:");
            Console.WriteLine(" * - Additioal bomb");
            Console.WriteLine(" L - Additional life");
            Console.WriteLine(" T - Additional time");
            Console.WriteLine(" % - doubles your current score");
            Console.WriteLine(" + - artifact that gives you 200 points");
            Console.WriteLine(" © - coin");
            Console.WriteLine(" There are some danger cells on field:");
            Console.WriteLine(" U - pit (drop a bomb to get out of it)");
            Console.WriteLine(" X - trap (you lose one life if step on it)");
            Console.WriteLine(" There is shop $, where you can buy items using coins");
            Console.WriteLine(" Win condition: destroy all bricks(#) to win");
            Console.WriteLine(" If you run out of bombs, time or lives - you lose");
            Console.WriteLine(" Good luck!");
            Console.WriteLine(" Press any key to return to menu");
            Console.ReadKey(true);
            DisplayMenu();
        }

        private void MenuSelector()
        {
            NewMenu = true;
            int row = MENU_SELECTOR_UPPER;  
            Console.SetCursorPosition(0, row);
            Console.Write(">");
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (NewMenu)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    
                        if (row > MENU_SELECTOR_UPPER)
                        {
                            Console.SetCursorPosition(0, row--);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row < MENU_SELECTOR_BOTTOM)
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
            Console.WriteLine(" Level 4");
            Console.WriteLine(" Generate random level");
            Console.WriteLine(" Continue from last save");
            Console.WriteLine(" Back");
            LevelSelector();
        }

        private void LevelSelector()
        {
            NewMenu = true;
            int row = LEVEL_SELECTOR_UPPER;
            Console.SetCursorPosition(0, row);
            Console.Write(">");
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (NewMenu)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (row > LEVEL_SELECTOR_UPPER)
                        {
                            Console.SetCursorPosition(0, row--);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row < LEVEL_SELECTOR_BOTTOM)
                        {
                            Console.SetCursorPosition(0, row++);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, 9);
                        Console.Write("Loading...");
                        ChooseLevel(row);
                        NewMenu = false;
                        break;
                }
            }
        }
        /*
         * 
        
        private void ChooseLevel(int row)
        {
            string json = "";

            switch (row)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    json = System.IO.File.ReadAllText($@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level{row}.json");
                    Level.Deserializer(json);
                    break;
                case 5:
                    json = "random";
                    DisplayMatrixSizes();
                    break;
                case 6:
                    json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\Save.json");
                    Level.Deserializer(json);
                    break;
                case 7:
                    DisplayMenu();
                    break;
            }
                
        }
*/

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
                    json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\level4.json");
                    break;
                case 5:
                    json = "random";
                    break;
                case 6:
                    json = System.IO.File.ReadAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\Save.json");
                    break;
                case 7:
                    DisplayMenu();
                    break;
            }

            if(json.Equals("random"))
            {
                DisplayMatrixSizes();
            }
            else
                Level.Deserializer(json);
        }

        private void DisplayMatrixSizes()
        {
            Console.Clear();
            Console.SetCursorPosition(3, 0);
            Console.WriteLine("Choose field size:");
            Console.WriteLine(" 6x6");
            Console.WriteLine(" 7x7");
            Console.WriteLine(" 8x8");
            Console.WriteLine(" 9x9");
            Console.WriteLine(" 10x10");
            MatrixSizeSelector();
        }

        private void MatrixSizeSelector()
        {
            NewMenu = true;
            int row = MENU_SELECTOR_UPPER;
            Console.SetCursorPosition(0, row);
            Console.Write(">");
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while(NewMenu)
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (row > MATRIX_SELECTOR_UPPER)
                        {
                            Console.SetCursorPosition(0, row--);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (row < MATRIX_SELECTOR_BOTTOM)
                        {
                            Console.SetCursorPosition(0, row++);
                            Console.Write(" ");
                            Console.SetCursorPosition(0, row);
                            Console.Write(">");
                        }
                        break;
                    case ConsoleKey.Enter:
                        ChooseMatrixSize(row);
                        NewMenu = false;
                        break;
                }
            }
        }

        private void ChooseMatrixSize(int row)
        {
            Session session;
            session = new Session(row + 5);
        }
    }
}
