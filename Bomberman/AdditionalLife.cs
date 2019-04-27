using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class AdditionalLife : SpecialElem
    {
        public AdditionalLife() : base('L', "addLife", false)
        {

        }

        public new void Effect(Session caller)
        {
            caller.Lives++;
            caller.DisplayLives();
        }
    }
}
