using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class SpecialElem : Elem
    {
        public SpecialElem(char sym, string name, bool constant) : base(sym, name, true, true, ConsoleColor.Magenta, constant)
        {

        }
    }
}
