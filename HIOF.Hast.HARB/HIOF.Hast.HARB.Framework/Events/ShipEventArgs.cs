using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
	public class ShipEventArgs(Ship ship) : EventArgs
	{
		public Ship Ship { get; } = ship;
	}
}
