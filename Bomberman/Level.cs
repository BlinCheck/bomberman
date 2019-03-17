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
        public Matrix matrix;

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
                PlayerY = 3;
                Minutes = 3;
                Seconds = 40;
                matrix = new Matrix
                {
                    mas = MakeLevelThreeMatrix()
                };
                matrix.Columns = 9;
                matrix.Rows = 9;
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
                {artifact, concrete, concrete, player, space, brick, concrete, concrete, concrete, brick},
                {space, addBomb, concrete, concrete, space, brick, space, brick, concrete, brick},
                {brick, concrete, brick, brick, brick, concrete, brick, brick, concrete, addLife},
                {brick, concrete, space, concrete, brick, concrete, concrete, space, concrete, brick},
                {space, brick, brick, concrete, space, brick, brick, space, brick, space}
            };
            return mas;
        }
    }
}
