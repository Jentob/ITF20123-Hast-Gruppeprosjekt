namespace HIOF.Hast.HARB.Framework
{
    public class Driver
    {
		/// <summary>Runs the simulation.</summary>
		/// <param name="daysToRun">The amount of days you wish to simulate</param>
		public static void Run(Harbor harbor, DateTime startTime, DateTime endTime)
        {
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
