using System;
using System.Threading;

namespace Bomberman
{
    class Program
    {
        
        public static string playerName;

        static void Main(string[] args)
        {
            Session session = new Session();
            session.Start();

            //Time time = new Time(2, 30);
            
        }


        /*public static void pickUpBonus(Elem elem)
        {
            if (elem.Name.Equals("addBomb"))
            {
                Session.bombAmount++;
                displayBombAmount();
            }

            if (elem.Name.Equals("addLife"))
            {
                Session.lives++;
                displayLives();
            }
        }*/



        /*public static void finalScore()
        {
            Session.score += Time.seconds * 5 + Time.minutes * 300;
            Session.score += Session.lives * 500;
            Session.score += Session.bombAmount * 300;
        }*/

        /*public static void setBomb(Matrix matrix)
        {
            Session.bombAmount--;
            displayBombAmount();
            Console.SetCursorPosition(playerY, playerX);
            Console.Write('@');
            matrix[playerX, playerY] = new Elem('@', "bomb", true, false);
            bombOn = true;
            bombX = playerX;
            bombY = playerY;
            TimerCallback timerCB = new TimerCallback(explodeBomb);
            Timer t = new Timer(timerCB, matrix, 1500, -1);
        }*/

        /*public static void explodeBomb(object obj)
        {
            Matrix matrix = (Matrix)obj;
            bool playerIsFound = false;
            if(bombX-1 >= 0 && matrix[bombX-1, bombY].Destroyable == true)
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

            if (bombX + 1 <= 4 && matrix[bombX+1, bombY].Destroyable == true)
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

            if (bombY - 1 >= 0 && matrix[bombX, bombY-1].Destroyable == true)
            {
                if (matrix[bombX, bombY - 1].Name.Equals("brick"))
                {
                    Session.brickAmount--;
                    Session.score += 100;
                }

                matrix[bombX, bombY-1] = new Elem('.', "ruine", false, false);
                Console.SetCursorPosition(bombY-1, bombX);
                Console.Write('.');
            }

            if (bombY + 1 <= 9 && matrix[bombX, bombY+1].Destroyable == true)
            {
                if (matrix[bombX, bombY + 1].Name.Equals("brick"))
                {
                    Session.brickAmount--;
                    Session.score += 100;
                }
                matrix[bombX, bombY+1] = new Elem('.', "ruine", false, false);
                Console.SetCursorPosition(bombY+1, bombX);
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

        /*public static void clearRuines(object obj)
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
