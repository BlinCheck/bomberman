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
        public int BombX;
        public int BombY;
        public string PlayerName;
        public int[,] Ruines = new int[5, 2];
        Matrix matrix;
        Time time;
        Timer bombExploder;
        Timer ruineCleaner;

        public Session()
        {
            matrix = new Matrix();
            matrix.GenerateMatrix();
            time = new Time(2, 30, this);

            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    Ruines[i, j] = -1;
                }
            }
        }

        public void End()
        {
            IsAlive = false;
            Console.SetCursorPosition(0, 6);
            Console.WriteLine("Game Over :(");
            Console.WriteLine("Press \"Q\" to quit");
        }

        public void Win()
        {
            IsAlive = false;
            FinalScore();
            DisplayScore();
            Console.SetCursorPosition(0, 6);
            Console.WriteLine($"Congratulations, {PlayerName}!");
            Console.WriteLine("You passed the level 1");
            Console.WriteLine("Press \"Q\" to quit");
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

        public void Move()
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
                                if (PlayerX - 1 >= 0 && matrix[PlayerX - 1, PlayerY].Step)
                                {
                                    Step(-1, 0);
                                }
                                break;
                            case ConsoleKey.D:
                                if (PlayerY + 1 <= 9 && matrix[PlayerX, PlayerY + 1].Step)
                                {
                                    Step(0, 1);
                                }
                                break;
                            case ConsoleKey.A:
                                if (PlayerY - 1 >= 0 && matrix[PlayerX, PlayerY - 1].Step)
                                {
                                    Step(0, -1);
                                }
                                break;
                            case ConsoleKey.S:
                                if (PlayerX + 1 <= 4 && matrix[PlayerX + 1, PlayerY].Step)
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

        public void MoveFromBomb(Matrix matrix, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.W:
                    if (PlayerX - 1 >= 0 && matrix[PlayerX - 1, PlayerY].Step)
                    {
                        StepFromBomb(-1, 0);
                    }
                    break;
                case ConsoleKey.D:
                    if (PlayerY + 1 <= 9 && matrix[PlayerX, PlayerY + 1].Step)
                    {
                        StepFromBomb(0, 1);
                    }
                    break;
                case ConsoleKey.A:
                    if (PlayerY - 1 >= 0 && matrix[PlayerX, PlayerY - 1].Step)
                    {
                        StepFromBomb(0, -1);
                    }
                    break;
                case ConsoleKey.S:
                    if (PlayerX + 1 <= 4 && matrix[PlayerX + 1, PlayerY].Step)
                    {
                        StepFromBomb(1, 0);
                    }
                    break;
            }
        }

        public void Step(int difX, int difY)
        {
            if (PlayerX+difX >= 0 && PlayerX+difX <= 4 &&
                PlayerY+difY >=0 && PlayerY+difY <= 9 && matrix[PlayerX+difX, PlayerY+difY].Name.Equals("space"))
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
        }

        public void StepFromBomb(int difX, int difY)
        {
            if (PlayerX + difX >= 0 && PlayerX + difX <= 4 &&
                PlayerY + difY >= 0 && PlayerY + difY <= 9 && matrix[PlayerX + difX, PlayerY + difY].Step)
            {
                PlayerX += difX;
                PlayerY += difY;
                matrix[PlayerX, PlayerY] = new Player();
                Console.SetCursorPosition(PlayerY, PlayerX);
                Console.Write('I');
            }
        }


        public void SetBomb()
        {
            BombAmount--;
            DisplayBombAmount();
            Console.SetCursorPosition(PlayerY, PlayerX);
            Console.Write('@');
            matrix[PlayerX, PlayerY] = new Bomb();
            BombOn = true;
            BombX = PlayerX;
            BombY = PlayerY;

            bombExploder = new Timer(1500);
            bombExploder.Elapsed += ExplodeBomb;
            bombExploder.Enabled = true;
            bombExploder.AutoReset = false;

        }

        public void ExplodeBomb(Object sender, ElapsedEventArgs e)
        {
            BombOn = false;

            if(BombY + 1 <= 9)
                DestroyElem(0, BombX, BombY + 1);

            if (BombX + 1 <= 4)
                DestroyElem(1, BombX + 1, BombY);

            if (BombX - 1 >= 0)
                DestroyElem(2, BombX - 1, BombY);

            if (BombY - 1 >= 0)
                DestroyElem(3, BombX, BombY - 1);

            DestroyElem(4, BombX, BombY);

            ruineCleaner = new Timer(500);
            ruineCleaner.Elapsed += ClearRuines;
            ruineCleaner.Enabled = true;
            ruineCleaner.AutoReset = false;

            if (PlayerIsDead)
                End();
        }


        public void DestroyElem(int row, int elemX, int elemY)
        {
            if(matrix[elemX, elemY].Destroyable)
            {
                Ruines[row, 0] = elemX;
                Ruines[row, 1] = elemY;

                if (elemX == PlayerX && elemY == PlayerY)
                    PlayerIsDead = true;

                matrix[elemX, elemY] = new Ruine();
                Console.SetCursorPosition(elemY, elemX);
                Console.Write('.');
            }

        }

        public void ClearRuines(Object sender, ElapsedEventArgs e)
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

        public void DisplayLives()
        {
            Console.SetCursorPosition(13, 1);
            Console.Write($"Lives: {Lives}");
        }

        public void DisplayBombAmount()
        {
            Console.SetCursorPosition(13, 2);
            if (BombAmount > 9)
                Console.Write($"Bombs: {BombAmount}");
            else
                Console.Write($"Bombs: {BombAmount} ");
        }

        public void FinalScore()
        {
            Score += time.Seconds * 5 + time.Minutes * 300;
            Score += Lives * 500;
            Score += BombAmount * 300;
        }

        public void DisplayScore()
        {
            Console.SetCursorPosition(13, 3);
            Console.Write($"Score: {Score}");
        }

        public void DisplayManual()
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

        /*public void ClearRuines(object obj)
        {
            Matrix matrix = (Matrix)obj;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (matrix[i, j].Name.Equals("ruine"))
                    {
                        matrix[i, j] = new Elem(' ', "space", true, true);
                        Console.SetCursorPosition(j, i);
                        Console.Write(' ');
                    }
                }
            }
        }*/
    }
}
