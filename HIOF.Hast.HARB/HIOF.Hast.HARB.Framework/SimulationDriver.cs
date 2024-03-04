namespace HIOF.Hast.HARB.Framework
{
    public class Driver
    {
		// Sailing events 
		// private static void Harbor_ShipSailed(object source, ShipEventArgs e)
		// {
		// // Print a message when a ship sails from port
		//     Console.WriteLine($"Ship {e.Ship.Name} has sailed from the port in {e.Ship.Destination}");
		// }
		// private static void Harbor_ShipArrived(object source, ShipEventArgs e)
		// {
		// // Print a message when a ship arrives
		//     Console.WriteLine($"Ship {e.Ship.Name} arrived at port in {e.Ship.Destination}");
		// }

		/// <summary>
		/// Runs the simulation.
		/// </summary>
		/// <param name="harbor">A harbor object to run the simulation on.</param>
		/// <param name="startTime">The date the simulation starts.</param>
		/// <param name="endTime">The date the simulation ends.</param>
		public static void Run(Harbor harbor, DateTime startTime, DateTime endTime)
        {
			// events
			// harbor.ShipSailing += Harbor_ShipSailed;
			// harbor.ShipArrived += Harbor_ShipArrived;

			DateTime time = startTime;
            while (time < endTime)
            {
                Update(harbor, time);
                time = time.AddMinutes(1);

                if (time == time.Date)
                    Console.Write($"\r{time:dd.MM.yyyy} / {endTime:dd.MM.yyyy}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Specifies what should happen during a cycle.
        /// </summary>
        /// <param name="harbor">The <see cref="Harbor"/> to update.</param>
        /// <param name="time">The time to update to.</param>
        private static void Update(Harbor harbor, DateTime time)
        {
            harbor.DockShips(time);
            harbor.OffloadCargoFromShips(time);
            harbor.LoadCargoToShips(time);
            harbor.ReleaseShips(time);
            harbor.QueueShips(time);
        }
    }
}
