using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Timer = System.Timers.Timer;

namespace Bomberman
{
    class Session
    {
        public bool ConsoleIsLocked = false;
        public bool IsAlive { get; set; }
        public bool PlayerIsDead = false;
        public int BrickAmount { get; set; }
        public int BombAmount { get; set; }
        public int Coins { get; set; }
        public int Lives { get; set; }
        public int Score { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public bool BombOn = false;
        public bool OnConstant { get; set; }
        public Tuple<int, int> BombPosition;
        public string PlayerName { get; set; }
        public int[,] Ruines = new int[5, 2];
        public Matrix matrix;
        public Time time;
        Timer bombExploder;
        Timer ruineCleaner;
        Timer messageCleaner;
        Level level;

        public Session(int size)
        {
            matrix = new Matrix();
            BrickAmount = matrix.GenerateRandomMatrix(size);
            time = new Time(3, 0, this);
            PlayerX = 0;
            PlayerY = 0;
            BombAmount = BrickAmount - 4;
            Lives = 2;
            Score = 0;
            matrix.Rows = size - 1;
            matrix.Columns = size - 1;
            level = new Level();
            level.PlayerX = PlayerX;
            level.PlayerY = PlayerY;
            InitRuines();
            Start();
        }

        public Session(Level level)
        {
            this.level = level;
            time = new Time(level.Minutes, level.Seconds, this);
            InitRuines();
            Lives = level.Lives;
            BrickAmount = level.BrickAmount;
            BombAmount = level.BombAmount;
            PlayerX = level.PlayerX;
            PlayerY = level.PlayerY;
            matrix = level.matrix;
            Score = level.Score;
        }

        private void InitRuines()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Ruines[i, j] = -1;
                }
            }
        }

        public void Save()
        {
            level.Save(this);
            LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, matrix.Rows + 2);
            Console.Write("Progress succesfully saved");
            UnlockConsole();

            messageCleaner = new Timer(2000);
            messageCleaner.Elapsed += ClearMessage;
            messageCleaner.Enabled = true;
            messageCleaner.AutoReset = false;

        }

        public void End()
        {
            time.t.Elapsed -= time.DisplayTimer;
            IsAlive = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, matrix.Rows+3);
            Console.WriteLine("Game Over :(");
            Console.WriteLine("Press any key to return to menu");
        }

        private void Win()
        {
            time.t.Elapsed -= time.DisplayTimer;
            IsAlive = false;
            FinalScore();
            DisplayScore();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, matrix.Rows+3);
            Console.WriteLine($"Congratulations, {PlayerName}!");
            Console.WriteLine("You passed the level");
            Console.WriteLine("Press any key to return to menu");
        }

        public void Start()
        {
            IsAlive = true;
            Console.Clear();
            Console.Write(" Enter your name: ");
            PlayerName = Console.ReadLine();
            Console.WriteLine($"\n Welcome, {PlayerName}!");
            Console.WriteLine(" If you are ready press any key to start");
            Console.ReadKey(true);
            Console.Clear();

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            matrix.DisplayMatrix();
            time.DisplayTimer(null, null);
            time.t.Elapsed += time.DisplayTimer;
            time.t.Enabled = true;
            time.t.AutoReset = true;
            DisplayLives();
            DisplayScore();
            DisplayBombAmount();

            Move();
            Menu menu = new Menu();
            menu.DisplayMenu();
        }

        private void Move()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (true)
            {
                if (IsAlive)
                {
                    key = Console.ReadKey(true);

                    if (matrix[PlayerX, PlayerY].IsConstant)
                        MoveFromConstant(key);
                    else
                    {
                        switch (key.Key)
                        {
                            case ConsoleKey.W:
                                if (PlayerX - 1 >= 0 && matrix[PlayerX - 1, PlayerY].Walkable)
                                {
                                    Step(-1, 0);
                                }
                                break;
                            case ConsoleKey.D:
                                if (PlayerY + 1 <= matrix.Columns && matrix[PlayerX, PlayerY + 1].Walkable)
                                {
                                    Step(0, 1);
                                }
                                break;
                            case ConsoleKey.A:
                                if (PlayerY - 1 >= 0 && matrix[PlayerX, PlayerY - 1].Walkable)
                                {
                                    Step(0, -1);
                                }
                                break;
                            case ConsoleKey.S:
                                if (PlayerX + 1 <= matrix.Rows && matrix[PlayerX + 1, PlayerY].Walkable)
                                {
                                    Step(1, 0);
                                }
                                break;
                            case ConsoleKey.E:
                                if (BombOn == false)
                                    SetBomb();
                                break;
                            case ConsoleKey.B:
                                Save();
                                break;
                            case ConsoleKey.Q:
                                ReturnToMenu();
                                break;
                        }
                    }
                }
                else
                    return;
            }
        }

        private void ReturnToMenu()
        {
            time.t.Elapsed -= time.DisplayTimer;
            IsAlive = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, matrix.Rows + 3);
            Console.Write("Returning to menu...");
            Thread.Sleep(1000);
        }

        private void MoveFromConstant(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.W:
                    if (PlayerX - 1 >= 0 && matrix[PlayerX - 1, PlayerY].Walkable)
                    {
                        StepFromConstant(-1, 0);
                    }
                    break;
                case ConsoleKey.D:
                    if (PlayerY + 1 <= matrix.Columns && matrix[PlayerX, PlayerY + 1].Walkable)
                    {
                        StepFromConstant(0, 1);
                    }
                    break;
                case ConsoleKey.A:
                    if (PlayerY - 1 >= 0 && matrix[PlayerX, PlayerY - 1].Walkable)
                    {
                        StepFromConstant(0, -1);
                    }
                    break;
                case ConsoleKey.S:
                    if (PlayerX + 1 <= matrix.Rows && matrix[PlayerX + 1, PlayerY].Walkable)
                    {
                        StepFromConstant(1, 0);
                    }
                    break;
            }
        }

        private void Step(int difX, int difY)
        {
            matrix[PlayerX, PlayerY] = new Space();
            LockConsole();
            Console.SetCursorPosition(PlayerY, PlayerX);
            Console.Write(' ');
            UnlockConsole();

            PlayerX += difX;
            PlayerY += difY;

            matrix[PlayerX, PlayerY].Effect(this);
            CheckEffect(PlayerX, PlayerY);
            if (!matrix[PlayerX, PlayerY].IsConstant)
            {
                matrix[PlayerX, PlayerY] = new Player();
                LockConsole();
                Console.SetCursorPosition(PlayerY, PlayerX);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('I');
                UnlockConsole();
            }
            else
            {
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                key = Console.ReadKey(true);
                MoveFromConstant(key);
            }
        }

        private void StepFromConstant(int difX, int difY)
        {
            PlayerX += difX;
            PlayerY += difY;
            CheckEffect(PlayerX, PlayerY);

            if (!matrix[PlayerX, PlayerY].IsConstant)
            {
                LockConsole();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(PlayerY, PlayerX);
                Console.Write('I');
                UnlockConsole();
            }
            else
            {
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                key = Console.ReadKey(true);
                MoveFromConstant(key);
            }
            
        }

        private void CheckEffect(int x, int y)
        {
            switch (matrix[x, y].Name)
            {
                case "addBomb":
                    new AdditionalBomb().Effect(this);
                    break;
                case "addLife":
                    new AdditionalLife().Effect(this);
                    break;
                case "artifact":
                    new Artifact().Effect(this);
                    break;
                case "scoreMultiplier":
                    new ScoreMultiplier().Effect(this);
                    break;
                case "addTime":
                    new AdditionalTime().Effect(this);
                    break;
                case "coin":
                    new Coin().Effect(this);
                    break;
                case "shop":
                    new Shop().Effect(this);
                    break;
                case "trap":
                    new Trap().Effect(this);
                    break;
                case "bombDrop":
                    new BombDrop().Effect(this);
                    break;
            }
        }

        private void SetBomb()
        {
            BombAmount--;
            DisplayBombAmount();

            LockConsole();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(PlayerY, PlayerX);
            Console.Write('@');
            UnlockConsole();

            matrix[PlayerX, PlayerY] = new Bomb();
            BombOn = true;
            BombPosition = new Tuple<int, int>(PlayerX, PlayerY);

            bombExploder = new Timer(1500);
            bombExploder.Elapsed += ExplodeBomb;
            bombExploder.Enabled = true;
            bombExploder.AutoReset = false;

        }

        private void ClearMessage(Object sender, ElapsedEventArgs e)
        {
            LockConsole();
            Console.SetCursorPosition(0, matrix.Rows + 2);
            Console.Write("                                       ");
            UnlockConsole();
        }

        private void ExplodeBomb(Object sender, ElapsedEventArgs e)
        {
            BombOn = false;

            if (BombPosition.Item2 + 1 <= matrix.Columns)
            {
                DestroyElem(0, new Tuple<int, int>(BombPosition.Item1, BombPosition.Item2 + 1));
            }

            if (BombPosition.Item1 + 1 <= matrix.Rows)
            {
                DestroyElem(1, new Tuple<int, int>(BombPosition.Item1 + 1, BombPosition.Item2));
            }

            if (BombPosition.Item1 - 1 >= 0)
            {
                DestroyElem(2, new Tuple<int, int>(BombPosition.Item1 - 1, BombPosition.Item2));
            }

            if (BombPosition.Item2 - 1 >= 0)
            {
                DestroyElem(3, new Tuple<int, int>(BombPosition.Item1, BombPosition.Item2 - 1));
            }

            DestroyElem(4, BombPosition);

            ruineCleaner = new Timer(500);
            ruineCleaner.Elapsed += ClearRuines;
            ruineCleaner.Enabled = true;
            ruineCleaner.AutoReset = false;

            DisplayScore();

            if (PlayerIsDead)
                End();

            if (BombAmount <= 0 && BrickAmount > 0)
                End();

            if (BrickAmount <= 0)
                Win();
        }


        private void DestroyElem(int row, Tuple<int, int> position)
        {
            if(PlayerX == position.Item1 && PlayerY == position.Item2)
            {
                Lives--;
                DisplayLives();
                if (Lives != 0)
                {
                    PlayerX = level.PlayerX;
                    PlayerY = level.PlayerY;
                    matrix[PlayerX, PlayerY] = new Player();

                    LockConsole();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(PlayerY, PlayerX);
                    Console.Write("I");
                    UnlockConsole();
                }
                else
                {
                    PlayerIsDead = true;
                }
            }

            if(matrix[position.Item1, position.Item2].Destroyable)
            {
                if(matrix[position.Item1, position.Item2].Name.Equals("brick"))
                {
                    Score += 100;
                    BrickAmount--;
                }

                Ruines[row, 0] = position.Item1;
                Ruines[row, 1] = position.Item2;

                matrix[position.Item1, position.Item2] = new Ruine();

                LockConsole();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(position.Item2, position.Item1);
                Console.Write('.');
                UnlockConsole();
            }

        }

        private void ClearRuines(Object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Ruines[i, 0] != -1 && Ruines[i, 1] != -1)
                {
                    matrix[Ruines[i, 0], Ruines[i, 1]] = new Space();

                    LockConsole();
                    Console.SetCursorPosition(Ruines[i, 1], Ruines[i, 0]);
                    Console.Write(' ');
                    UnlockConsole();

                    Ruines[i, 0] = -1;
                    Ruines[i, 1] = -1;
                }
            }
        }

        public void DisplayLives()
        {
            LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(matrix.Columns + 3, 1);
            Console.Write($"Lives: {Lives}");
            UnlockConsole();
        }

        public void DisplayBombAmount()
        {
            LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(matrix.Columns + 3, 2);
            if (BombAmount > 9)
                Console.Write($"Bombs: {BombAmount}");
            else
                Console.Write($"Bombs: {BombAmount} ");

            UnlockConsole();
        }

        public void DisplayCoins()
        {
            LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(matrix.Columns + 3, 4);
            Console.Write($"Coins: {Coins}");
            UnlockConsole();
        }

        private void FinalScore()
        {
            Score += time.Seconds * 5 + time.Minutes * 300;
            Score += Lives * 500;
            Score += BombAmount * 300;
            Score += Coins * 350;
        }

        public void DisplayScore()
        {
            LockConsole();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(matrix.Columns+3, 3);
            Console.Write($"Score: {Score}");
            UnlockConsole();
        }

        public void LockConsole()
        {
            while(ConsoleIsLocked)
            {

            }

            ConsoleIsLocked = true;
        }

        public void UnlockConsole()
        {
            ConsoleIsLocked = false;
        }
    }
}
