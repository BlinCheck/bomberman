using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class ScoreMultiplier : SpecialElem
    {
        public ScoreMultiplier() : base('%', "scoreMultiplier", false)
        {

        }

        public new void Effect(Session caller)
        {
            caller.Score *= 2;
            caller.DisplayScore();
        }
    }
}
