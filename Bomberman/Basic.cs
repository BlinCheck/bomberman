using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    abstract class Basic
    {
        public char Symbol { get; set; }
        public string Name { get; set; }
        public bool Destroyable { get; set; }

        public Basic()
        {

        }
    }
}
