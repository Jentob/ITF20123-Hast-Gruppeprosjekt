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
                time.AddMinutes(1);
            }
        }

		private static void Update(Harbor harbor, DateTime time)
		{
			// TODO: ???
		}
	}
}
