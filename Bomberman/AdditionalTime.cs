using System;
using System.Collections.Generic;
using System.Text;

namespace Bomberman
{
    class AdditionalTime : SpecialElem
    {
        public AdditionalTime() : base('T', "addTime", false)
        {

        }

        public new void Effect(Session caller)
        {
            caller.time.Seconds += 30;
            caller.time.DisplayTimer(null, null);
        }
    }
}
