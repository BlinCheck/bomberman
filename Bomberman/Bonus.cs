using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Bonus : Elem
    {
        public Bonus(char sym, string name) : this ()
        {
            Symbol = sym;
            Name = name;
            Color = ConsoleColor.Magenta;
        }

        public Bonus() : base(true, true)
        {

        }
    }
}
