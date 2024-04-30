namespace HIOF.Hast.HARB.Framework
{
    /// <summary>
    /// Driver for running simulation of harbor.
    /// </summary>
	/// <param name="harbor">The harbor object to run the simulation on.</param>
	public class SimulationDriver(Harbor harbor)
    {

		/// <summary>
		/// The harbor object to run the simulation on.
		/// </summary>
		public Harbor Harbor { get; set; } = harbor;

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        /// <param name="startTime">The date the simulation starts.</param>
        /// <param name="endTime">The date the simulation ends.</param>
        public void Run(DateTime startTime, DateTime endTime)
        {
            harbor.InitializeAllAgvs();


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
