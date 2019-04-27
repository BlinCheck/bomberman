using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Trap : SpecialElem
    {
        public Trap() : base('X', "trap", true)
        {

        }

        public new void Effect(Session caller)
        {
            caller.Lives--;
            caller.DisplayLives();
            if (caller.Lives == 0)
            {
                caller.End();
            }
        }
    }
}
