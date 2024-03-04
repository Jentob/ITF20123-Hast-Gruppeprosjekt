using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework.Events
{
    public class ShipArrivedEventArgs : EventArgs
    {
        public ShipArrivedEventArgs (Ship shipArrived)
        {
            ShipArrived = shipArrived;
        }

        public Ship ShipArrived { get; private set; }
    }
}