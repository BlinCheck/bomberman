using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Session
    {
        public bool IsAlive;
        public bool PlayerIsDead = false;
        public int BrickAmount = 21;
        public int BombAmount = 14;
        public int Lives = 2;
        public int Score = 0;
        public int PlayerX = 0;
        public int PlayerY = 0;
        public bool BombOn = false;
        public Tuple<int, int> BombPosition;
        public string PlayerName;
        public int[,] Ruines = new int[5, 2];
        public Matrix matrix;
        Time time;
        Timer bombExploder;
        Timer ruineCleaner;
        Level level;

        public Session()
        {
            matrix = new Matrix();
            matrix.GenerateMatrix();
            time = new Time(2, 30, this);
            InitRuines();
        }

        public Session(Level level)
        {
            this.level = level;
            matrix = level.matrix;
            time = new Time(level.Minutes, level.Seconds, this);
            InitRuines();
            Lives = level.Lives;
            BrickAmount = level.BrickAmount;
            BombAmount = level.BombAmount;
            PlayerX = level.PlayerX;
            PlayerY = level.PlayerY;
            matrix = level.matrix;
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

        public void End()
        {
            IsAlive = false;
            Console.SetCursorPosition(0, matrix.Rows+3);
            Console.WriteLine("Game Over :(");
            Console.WriteLine("Press any key to return to menu");
            time.t.Elapsed -= time.DisplayTimer;
        }

        private void Win()
        {
            IsAlive = false;
            FinalScore();
            DisplayScore();
            Console.SetCursorPosition(0, matrix.Rows+3);
            Console.WriteLine($"Congratulations, {PlayerName}!");
            Console.WriteLine("You passed the level 1");
            Console.WriteLine("Press any key to return to menu");
            time.t.Elapsed -= time.DisplayTimer;
        }

        public void Start()
        {
            DisplayManual();
            IsAlive = true;
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
        }

        private void Move()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            while (IsAlive && key.Key != ConsoleKey.Q)
            {
                if (IsAlive)
                {
                    key = Console.ReadKey(true);

                    if (matrix[PlayerX, PlayerY].Name.Equals("bomb"))
                        MoveFromBomb(matrix, key);
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
                        }
                    }
                }
            }
        }

        private void MoveFromBomb(Matrix matrix, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.W:
                    if (PlayerX - 1 >= 0 && matrix[PlayerX - 1, PlayerY].Walkable)
                    {
                        PickUpBonus(PlayerX-1, PlayerY);
                        StepFromBomb(-1, 0);
                    }
                    break;
                case ConsoleKey.D:
                    if (PlayerY + 1 <= matrix.Columns && matrix[PlayerX, PlayerY + 1].Walkable)
                    {
                        PickUpBonus(PlayerX, PlayerY+1);
                        StepFromBomb(0, 1);
                    }
                    break;
                case ConsoleKey.A:
                    if (PlayerY - 1 >= 0 && matrix[PlayerX, PlayerY - 1].Walkable)
                    {
                        PickUpBonus(PlayerX, PlayerY-1);
                        StepFromBomb(0, -1);
                    }
                    break;
                case ConsoleKey.S:
                    if (PlayerX + 1 <= matrix.Rows && matrix[PlayerX + 1, PlayerY].Walkable)
                    {
                        PickUpBonus(PlayerX+1, PlayerY);
                        StepFromBomb(1, 0);
                    }
                    break;
            }
        }

        private void Step(int difX, int difY)
        { 
            matrix[PlayerX, PlayerY] = new Space();
            Console.SetCursorPosition(PlayerY, PlayerX);
            Console.Write(' ');
            PlayerX += difX;
            PlayerY += difY;
            matrix[PlayerX, PlayerY] = new Player();
            Console.SetCursorPosition(PlayerY, PlayerX);
            Console.Write('I');
        }

        private void StepFromBomb(int difX, int difY)
        {
            PlayerX += difX;
            PlayerY += difY;
            matrix[PlayerX, PlayerY] = new Player();
            Console.SetCursorPosition(PlayerY, PlayerX);
            Console.Write('I');
            
        }

        private void PickUpBonus(int x, int y)
        {
            if(matrix[x, y].Name.Equals("addBomb"))
            {
                BombAmount++;
                DisplayBombAmount();
            }

            if (matrix[x, y].Name.Equals("addLife"))
            {
                Lives++;
                DisplayLives();
            }
        }

        private void SetBomb()
        {
            BombAmount--;
            DisplayBombAmount();
            Console.SetCursorPosition(PlayerY, PlayerX);
            Console.Write('@');
            matrix[PlayerX, PlayerY] = new Bomb();
            BombOn = true;
            BombPosition = new Tuple<int, int>(PlayerX, PlayerY);

            bombExploder = new Timer(1500);
            bombExploder.Elapsed += ExplodeBomb;
            bombExploder.Enabled = true;
            bombExploder.AutoReset = false;

        }

        private void ExplodeBomb(Object sender, ElapsedEventArgs e)
        {
            BombOn = false;

            if (BombPosition.Item2 + 1 <= 9)
            {
                DestroyElem(0, new Tuple<int, int>(BombPosition.Item1, BombPosition.Item2 + 1));
            }

            if (BombPosition.Item1 + 1 <= 4)
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
            if(matrix[position.Item1, position.Item2].Name.Equals("player"))
            {
                Lives--;
                DisplayLives();
                if (Lives == 0)
                {
                    PlayerIsDead = true;
                    Ruines[row, 0] = position.Item1;
                    Ruines[row, 1] = position.Item2;

                    matrix[position.Item1, position.Item2] = new Ruine();
                    Console.SetCursorPosition(position.Item2, position.Item1);
                    Console.Write('.');
                }
            }
            else
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
                Console.SetCursorPosition(position.Item2, position.Item1);
                Console.Write('.');
            }

        }

        private void ClearRuines(Object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Ruines[i, 0] != -1 && Ruines[i, 1] != -1)
                {
                    matrix[Ruines[i, 0], Ruines[i, 1]] = new Space();
                    Console.SetCursorPosition(Ruines[i, 1], Ruines[i, 0]);
                    Console.Write(' ');

                    Ruines[i, 0] = -1;
                    Ruines[i, 1] = -1;
                }
            }
        }

        private void DisplayLives()
        {
            Console.SetCursorPosition(matrix.Columns + 3, 1);
            Console.Write($"Lives: {Lives}");
        }

        private void DisplayBombAmount()
        {
            Console.SetCursorPosition(matrix.Columns + 3, 2);
            if (BombAmount > 9)
                Console.Write($"Bombs: {BombAmount}");
            else
                Console.Write($"Bombs: {BombAmount} ");
        }

        private void FinalScore()
        {
            Score += time.Seconds * 5 + time.Minutes * 300;
            Score += Lives * 500;
            Score += BombAmount * 300;
        }

        private void DisplayScore()
        {
            Console.SetCursorPosition(matrix.Columns+3, 3);
            Console.Write($"Score: {Score}");
        }

        private void DisplayManual()
        {
            Console.Clear();
            Console.WriteLine(" Welcome to Bomberman!");
            Console.WriteLine(" This is short manual for the game:");
            Console.WriteLine(" Use W, A, S, D to move");
            Console.WriteLine(" Use E to set the bomb");
            Console.WriteLine(" You can quit game by pressing Q");
            Console.WriteLine(" You can pick up additional bombs(*) and lives(L)");
            Console.WriteLine(" Destroy all bricks(#) to win");
            Console.WriteLine(" Press any key to continue");
            Console.ReadKey(true);
            Console.Clear();
        }

        /*public static void explodeBomb(object obj)
    {
        Matrix matrix = (Matrix)obj;
        bool playerIsFound = false;
        if (bombX - 1 >= 0 && matrix[bombX - 1, bombY].Destroyable == true)
        {
            if (matrix[bombX - 1, bombY].Name.Equals("brick"))
            {
                Session.brickAmount--;
                Session.score += 100;
            }
            matrix[bombX - 1, bombY] = new Elem('.', "ruine", false, false);
            Console.SetCursorPosition(bombY, bombX - 1);
            Console.Write('.');
        }

        if (bombX + 1 <= 4 && matrix[bombX + 1, bombY].Destroyable == true)
        {
            if (matrix[bombX + 1, bombY].Name.Equals("brick"))
            {
                Session.brickAmount--;
                Session.score += 100;
            }

            matrix[bombX + 1, bombY] = new Elem('.', "ruine", false, false);
            Console.SetCursorPosition(bombY, bombX + 1);
            Console.Write('.');
        }

        if (bombY - 1 >= 0 && matrix[bombX, bombY - 1].Destroyable == true)
        {
            if (matrix[bombX, bombY - 1].Name.Equals("brick"))
            {
                Session.brickAmount--;
                Session.score += 100;
            }

            matrix[bombX, bombY - 1] = new Elem('.', "ruine", false, false);
            Console.SetCursorPosition(bombY - 1, bombX);
            Console.Write('.');
        }

        if (bombY + 1 <= 9 && matrix[bombX, bombY + 1].Destroyable == true)
        {
            if (matrix[bombX, bombY + 1].Name.Equals("brick"))
            {
                Session.brickAmount--;
                Session.score += 100;
            }
            matrix[bombX, bombY + 1] = new Elem('.', "ruine", false, false);
            Console.SetCursorPosition(bombY + 1, bombX);
            Console.Write('.');
        }

        matrix[bombX, bombY] = new Elem('.', "ruine", false, false);
        Console.SetCursorPosition(bombY, bombX);
        Console.Write('.');
        bombOn = false;

        displayScore();

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
        {
            if (Session.lives > 1)
            {
                Session.lives--;
                displayLives();
                matrix[0, 0] = new Elem('I', "player", true, false);
                playerX = 0;
                playerY = 0;
                Console.SetCursorPosition(0, 0);
                Console.Write("I");
            }
            else
            {
                Session.lives--;
                displayLives();
                Session.End();
                return;
            }
        }

        if (Session.bombAmount == 0 && Session.brickAmount != 0)
            Session.End();

        if (Session.brickAmount <= 0)
            Session.Win();
    }*/

       
    }
}
