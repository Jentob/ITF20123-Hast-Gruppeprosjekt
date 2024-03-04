using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework.Events
{
    public class ShipSailingEventArgs : EventArgs
    {
        public ShipSailingEventArgs (Ship shipSailing)
        {
            ShipSailing = shipSailing;
        }

        public Ship ShipSailing { get; private set; }
    }
}
