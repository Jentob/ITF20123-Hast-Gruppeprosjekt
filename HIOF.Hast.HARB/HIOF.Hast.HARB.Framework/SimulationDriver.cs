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
        public Harbor Harb { get; set; } = harbor;

        /// <summary>
        /// 
        /// </summary>
        public bool PrintProgress { get; set; } = false;

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        /// <param name="startTime">The date the simulation starts.</param>
        /// <param name="endTime">The date the simulation ends.</param>
        public void Run(DateTime startTime, DateTime endTime)
        {
            Harb.InitializeAllAgvs();


            DateTime time = startTime;
            while (time < endTime)
            {
                Update(Harb, time);
                time = time.AddMinutes(1);

                if (PrintProgress && time == time.Date)
                {
                    Console.Write($"\r{time:dd.MM.yyyy} / {endTime:dd.MM.yyyy}");
                }
            }
            if (PrintProgress)
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Specifies what should happen during a cycle.
        /// </summary>
        /// <param name="harbor">The <see cref="Harbor"/> to update.</param>
        /// <param name="time">The time to update to.</param>
        private static void Update(Harbor harb, DateTime time)
        {
            harb.DockShips(time);
            harb.OffloadCargoFromShips(time);
            harb.LoadCargoToShips(time);
            harb.ReleaseShips(time);
            harb.QueueShips(time);
        }
    }
}
