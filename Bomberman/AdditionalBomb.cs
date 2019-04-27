using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class AdditionalBomb : SpecialElem
    {
        public AdditionalBomb() : base('*', "addBomb", false)
        {

        }

        public new void Effect(Session caller)
        {
            caller.BombAmount++;
            caller.DisplayBombAmount();
        }
    }
}
