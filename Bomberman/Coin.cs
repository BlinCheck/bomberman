using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Coin : SpecialElem
    {
        public Coin() : base ('©', "coin", false)
        {

        }

        public new void Effect(Session caller)
        {
            caller.Coins++;
            caller.DisplayCoins();
        }

    }
}
