using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Elem
    {
        public char Symbol { get; set; }
        public string Name { get; set; }
        public bool Destroyable { get; set; }
        public bool Step { get; set; }


        public Elem(char sym, string name, bool destroyable, bool step)
        {
            Symbol = sym;
            Name = name;
            Destroyable = destroyable;
            Step = step;
        }
    }
}
