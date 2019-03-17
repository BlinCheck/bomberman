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
        public bool Walkable { get; set; }
        public ConsoleColor Color { get; set; }


        public Elem(char sym, string name, bool destroyable, bool step, ConsoleColor color)
        {
            Symbol = sym;
            Name = name;
            Destroyable = destroyable;
            Walkable = step;
            Color = color;
        }
    }
}
