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
    }
}
