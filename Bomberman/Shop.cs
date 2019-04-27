using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Shop : SpecialElem
    {
        int row = 1;

        public Shop() : base('$', "shop", true)
        {

        }

        public new void Effect(Session caller)
        {
            DisplayShop(caller);
        }

        public void DisplayShop(Session session)
        {
            session.DisplayCoins();
            session.LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(session.matrix.Columns + 16, 0);
            Console.Write("Shop:");
            Console.SetCursorPosition(session.matrix.Columns + 16, 1);
            Console.Write(" Back");
            Console.SetCursorPosition(session.matrix.Columns + 16, 2);
            Console.Write(" Bomb (1 coin)");
            Console.SetCursorPosition(session.matrix.Columns + 16, 3);
            Console.Write(" Time (2 coins)");
            Console.SetCursorPosition(session.matrix.Columns + 16, 4);
            Console.Write(" Life (4 coins)");
            session.UnlockConsole();
            ItemSelector(session);
        }

        private void ItemSelector(Session session)
        {
            bool newShop = true;
            session.LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(session.matrix.Columns + 16, row);
            Console.Write(">");
            session.UnlockConsole();
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while(newShop)
            {
                key = Console.ReadKey(true);

                switch(key.Key)
                {
                    case ConsoleKey.W:
                        if (row > 1)
                        {
                            session.LockConsole();
                            Console.SetCursorPosition(session.matrix.Columns + 16, row--);
                            Console.Write(" ");
                            Console.SetCursorPosition(session.matrix.Columns + 16, row);
                            Console.Write(">");
                            session.UnlockConsole();
                        }
                        break;
                    case ConsoleKey.S:
                        if (row < 4)
                        {
                            session.LockConsole();
                            Console.SetCursorPosition(session.matrix.Columns + 16, row++);
                            Console.Write(" ");
                            Console.SetCursorPosition(session.matrix.Columns + 16, row);
                            Console.Write(">");
                            session.UnlockConsole();
                        }
                        break;
                    case ConsoleKey.Enter:
                        ChooseItem(session);
                        newShop = false;
                        break;
                }
            }
        }

        private void ChooseItem(Session session)
        {
            switch(row)
            {
                case 2:
                    if (session.Coins >= 1)
                    {
                        session.BombAmount++;
                        session.DisplayBombAmount();
                        session.Coins--;
                    }
                    break;
                case 3:
                    if(session.Coins >= 2)
                    {
                        session.time.Seconds += 40;
                        session.time.DisplayTimer(null, null);
                        session.Coins -= 2;
                    }
                    break;
                case 4:
                    if(session.Coins >= 4)
                    {
                        session.Lives++;
                        session.DisplayLives();
                        session.Coins -= 4;
                    }
                    break;
            }
            session.DisplayCoins();
            if (row == 1)
                Back(session);
            else
                ItemSelector(session);
        }

        private void Back(Session session)
        {
            session.LockConsole();
            Console.SetCursorPosition(session.matrix.Columns + 16, 0);
            Console.Write("                    ");
            Console.SetCursorPosition(session.matrix.Columns + 16, 1);
            Console.Write("                    ");
            Console.SetCursorPosition(session.matrix.Columns + 16, 2);
            Console.Write("                    ");
            Console.SetCursorPosition(session.matrix.Columns + 16, 3);
            Console.Write("                    ");
            Console.SetCursorPosition(session.matrix.Columns + 16, 4);
            Console.Write("                    ");
            session.UnlockConsole();
        }
    }
}
