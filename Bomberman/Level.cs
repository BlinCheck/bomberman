using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Level
    {
        public int BrickAmount { get; set; }
        public int BombAmount { get; set; }
        public int Lives { get; set; }
        public int PlayerX { get; set; }
        public int PlayerY { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Score { get; set; }
        public Matrix matrix;

        public Level()
        {

        }

        public static void Deserializer(string json)
        {
            Level level = new Level();
            level = JsonConvert.DeserializeObject<Level>(json);
            Session session = new Session(level);
            session.Start();
        }

        public void Save(Session session)
        {
            Minutes = session.time.Minutes;
            Seconds = session.time.Seconds;
            Lives = session.Lives;
            BrickAmount = session.BrickAmount;
            BombAmount = session.BombAmount;
            PlayerX = session.PlayerX;
            PlayerY = session.PlayerY;
            matrix = session.matrix;
            Score = session.Score;

            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(@"C:\Users\user\source\repos\Bomberman\Bomberman\levels\Save.json", json);
        }

        public Level(int number)
        {
            if(number == 2)
            {
                BrickAmount = 18;
                BombAmount = 14;
                Lives = 1;
                PlayerX = 0;
                PlayerY = 0;
                Minutes = 2;
                Seconds = 0;
                matrix = new Matrix
                {
                    mas = MakeLevelTwoMatrix()
                };
                matrix.Columns = 5;
                matrix.Rows = 6;
            }

            if(number == 1)
            {
                BrickAmount = 21;
                BombAmount = 16;
                Lives = 2;
                PlayerX = 0;
                PlayerY = 0;
                Minutes = 2;
                matrix = new Matrix
                {
                    mas = MakeLevelOneMatrix()
                };
                matrix.Columns = 9;
                matrix.Rows = 4;
            }

            if(number == 3)
            {
                BrickAmount = 40;
                BombAmount = 33;
                Lives = 2;
                PlayerX = 5;
                PlayerY = 2;
                Minutes = 3;
                Seconds = 40;
                matrix = new Matrix
                {
                    mas = MakeLevelThreeMatrix()
                };
                matrix.Columns = 9;
                matrix.Rows = 9;
            }

            if(number == 4)
            {
                BrickAmount = 35;
                BombAmount = 27;
                Lives = 2;
                PlayerX = 8;
                PlayerY = 4;
                Minutes = 3;
                Seconds = 0;
                matrix = new Matrix
                {
                    mas = MakeLevelFourMatrix(),
                    Columns = 9,
                    Rows = 9
                };
            }
        }

        private Elem[,] MakeLevelOneMatrix()
        {
            Space space = new Space();
            Player player = new Player();
            Brick brick = new Brick();
            Concrete concrete = new Concrete();

            Elem[,] mas = new Elem[,] {
                {player, space, space, brick, space, space, brick, brick, space, brick},
                {space, concrete, brick, concrete, space, concrete, space, concrete, space, concrete},
                {brick, brick, space, brick, brick, space, brick, brick, brick, brick},
                {space, concrete, brick, concrete, space, concrete, space, concrete, brick, concrete},
                {space, brick, brick, brick, space, space, brick, brick, brick, space}
            };
            return mas;
        }

        private Elem[,] MakeLevelTwoMatrix()
        {
            Space space = new Space();
            Player player = new Player();
            Brick brick = new Brick();
            Concrete concrete = new Concrete();
            AdditionalLife addLife = new AdditionalLife();
            AdditionalBomb addBomb = new AdditionalBomb();

            Elem[,] mas = new Elem[,] {
                {player, space, space, brick, brick, addBomb},
                {space, brick, brick, space, concrete, space},
                {concrete, space, brick, brick, addLife, space},
                {concrete, brick, concrete, concrete, concrete, brick},
                {brick, space, brick, concrete, addBomb, space},
                {brick, brick, brick, concrete, space, brick},
                {concrete, space, brick, brick, brick, brick}
            };
            return mas;
        }

        private Elem[,] MakeLevelThreeMatrix()
        {
            Space space = new Space();
            Player player = new Player();
            Brick brick = new Brick();
            Concrete concrete = new Concrete();
            AdditionalLife addLife = new AdditionalLife();
            AdditionalBomb addBomb = new AdditionalBomb();
            AdditionalTime addTime = new AdditionalTime();
            Artifact artifact = new Artifact();
            ScoreMultiplier scoreMultiplier = new ScoreMultiplier();

            Elem[,] mas = new Elem[,]
            {
                {brick, addTime, brick, space, brick, concrete, brick, brick, space, concrete},
                {brick, concrete, brick, concrete, concrete, concrete, scoreMultiplier, brick, space, brick},
                {space, brick, brick, brick, space, concrete, brick, concrete, concrete, space},
                {brick, concrete, addBomb, concrete, brick, concrete, space, brick, brick, brick},
                {space, brick, concrete, concrete, brick, addBomb, brick, concrete, space, artifact},
                {artifact, concrete, player, space, space, brick, concrete, concrete, concrete, brick},
                {space, addBomb, concrete, concrete, space, brick, space, brick, concrete, brick},
                {brick, concrete, brick, brick, brick, concrete, brick, brick, concrete, addLife},
                {brick, concrete, space, concrete, brick, concrete, concrete, space, concrete, brick},
                {space, brick, brick, concrete, space, brick, brick, space, brick, space}
            };
            return mas;
        }

        private Elem[,] MakeLevelFourMatrix()
        {
            Space space = new Space();
            Player player = new Player();
            Brick brick = new Brick();
            Concrete concrete = new Concrete();
            AdditionalBomb addBomb = new AdditionalBomb();
            Coin coin = new Coin();
            Shop shop = new Shop();
            Trap trap = new Trap();
            BombDrop bombDrop = new BombDrop();

            Elem[,] mas = new Elem[,]
            {
                {coin, space, brick, concrete, space, brick, space, concrete, concrete, shop},
                {concrete, concrete, space, concrete, coin, brick, brick, trap, brick, brick},
                {space, addBomb, brick, concrete, brick, space, concrete, space, concrete, brick},
                {concrete, concrete, brick, brick, space, bombDrop, brick, coin, concrete, space},
                {space, brick, space, concrete, brick, concrete, brick, concrete, addBomb, brick},
                {brick, coin, concrete, concrete, brick, concrete, space, brick, space, brick},
                {space, brick, brick, concrete, space, brick, brick, concrete, brick, concrete},
                {concrete, concrete, brick, brick, space, brick, space, concrete, coin, concrete},
                {space, space, brick, concrete, player, concrete, brick, brick, brick, space},
                {brick, coin, brick, concrete, concrete,concrete, brick, concrete, concrete, concrete}
            };

            return mas;
        }
    }
}
