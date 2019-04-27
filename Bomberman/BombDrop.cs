using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class BombDrop : SpecialElem
    {
        public BombDrop() : base('U', "bombDrop", true)
        {

        }

        public new void Effect(Session caller)
        {
            caller.BombAmount--;
            caller.DisplayBombAmount();
        }
    }
}
