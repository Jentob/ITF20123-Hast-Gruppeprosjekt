using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    internal class MinuteSteppedDriver : ISimulationDriver
    {
		/// <summary>
		/// Runs the simulation.
		/// The program start from current date/time
		/// </summary>
		/// <param name="daysToRun">The amount of days you wish to simulate</param>
		public void Run(Harbour harbor, int daysToRun)
        {
            DateTime time = DateTime.Now;
            while (time < time.AddDays(daysToRun))
            {
				Update(harbor, time);
                time.AddMinutes(1);
            }
        }

		private void Update(Harbour harbor, DateTime time)
		{
			foreach (Ship ship in harbor.Ships)
			{
				// Do something
			}
		}
	}
}
