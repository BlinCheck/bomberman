using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Artifact : SpecialElem
    {
        public Artifact() : base ('+', "artifact", false)
        {

        }

        public new void Effect(Session caller)
        {
            caller.Score += 200;
            caller.DisplayScore();
        }
    }
}
