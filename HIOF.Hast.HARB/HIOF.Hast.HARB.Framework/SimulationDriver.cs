namespace HIOF.Hast.HARB.Framework
{
    public class SimulationDriver
    {
        // Sailing events 
        private static void Harbor_ShipSailed(object? sender, ShipSailingEventArgs e)
        {
            Console.WriteLine($"Ship {e.ShipSailing} has sailed from the port in {e.ShipSailing.Destination}");
        }

        private static void Harbor_ShipArrived(object? sender, ShipArrivedEventArgs e)
        {
            // Print a message when a ship arrives
            Console.WriteLine($"Ship {e.ShipArrived} arrived at port in {e.ShipArrived.Destination}");
        }

        // Cargo loading events
        private static void Harbor_CargoLoaded(object? sender, ShipLoadingCargoEventArgs e)
        {
            Console.WriteLine($"Cargo {e.CargoLoaded} loading onto ship.");
        }

        private static void Harbor_CargoOffloaded(object? sender, ShipOffloadingCargoEventArgs e)
        {
            Console.WriteLine($"Cargo {e.CargoOffloaded} has been unloaded");
        }

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
            harbor.InitializeAllAGVs();
            harbor.OffloadCargoFromShips(time);
            harbor.LoadCargoToShips(time);
            harbor.ReleaseShips(time);
            harbor.QueueShips(time);
        }
    }
}
