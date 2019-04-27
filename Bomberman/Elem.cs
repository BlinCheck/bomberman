using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class Elem : Basic
    {
        public bool Walkable { get; set; }
        public bool IsConstant { get; set; }
        public ConsoleColor Color { get; set; }


        public Elem(char sym, string name, bool destroyable, bool step, ConsoleColor color, bool constant)
        {
            Symbol = sym;
            Name = name;
            Destroyable = destroyable;
            Walkable = step;
            Color = color;
            IsConstant = constant;
        }

        public void Effect(Session caller)
        {

        }
    }
}
