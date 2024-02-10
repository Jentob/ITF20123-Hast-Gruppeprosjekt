namespace HIOF.Hast.HARB.Framework
{
	/// <summary>Represents arbritary ship sizes.</summary>
	public enum ShipSize
	{
		Small,
		Medium,
		Large,
	}
	/// <summary>LogEntry keeps time and event separated.</summary>
	public struct LogEntry
	{
		public DateTime time;
		public string message;

		public LogEntry(DateTime time, string message) : this()
		{
			this.time = time;
			this.message = message;
		}
		internal LogEntry(DateTime? time, string message) : this()
		{
			if (time != null) this.time = (DateTime)time;
			else this.time = DateTime.MinValue;
			this.message = message;
		}
        public override readonly string ToString()
        {
            return $"{time} - {message}";
        }
    }

	/// <summary>Represents a ship able to hold cargo.</summary>
	/// <param name="name">Name of ship.</param>
	/// <param name="size">Size of ship.</param>
	/// <param name="maxCarryWeightInKG">Represents max cargo weight the ship is able to handle.</param>
	public class Ship(string name, ShipSize size, int maxCarryWeightInKG)
	{
		private static int idCount = 0;
		public int Id { get; } =idCount++;
		public string Name { get; set; } = name;
		public ShipSize Size { get; } = size;
		public List<LogEntry> Log { get; } = [];
		public List<ICargo> Cargohold { get; } = [];
		public int MaxCargoWeightInKG { get; } = maxCarryWeightInKG;
        public List<SailingSchedule> SailingSchedules { get; } = [];



        public void AddToSailingSchedule(DateTime departureTime, TimeSpan interval)
        {
            SailingSchedules.Add(new SailingSchedule(departureTime, interval));
        }

		/// <summary>Adds cargo to the cargohold</summary>
		/// <param name="cargo">The cargo to be loaded onboard the ship</param>
		public bool AddCargo(ICargo cargo, DateTime? time = null)
		{
			double weight = cargo.WeightInKG;
			foreach (var item in Cargohold)
				weight += item.WeightInKG;
			if (weight > MaxCargoWeightInKG)
				return false;
			
			if(time != null)
			{
				cargo.RecordHistory(new(time, $"Added to ship {Name}({Id})"));
				RecordHistory(new(time, $"{cargo.Name}({cargo.Id}) added to cargohold"));
			}

			Cargohold.Add(cargo);


			return true;
		}

		/// <summary>Remove cargo from the ship object.</summary>
		/// <param name="cargo"></param>
		/// <returns>Returns the cargo interface.</returns>
		public bool RemoveCargo(ICargo cargo, DateTime? time = null)
		{
			if (!Cargohold.Contains(cargo)) return false;
			Cargohold.Remove(cargo);
			if(time != null)
			{
				RecordHistory(new(time, $"{cargo.Name}({cargo.Id}) removed from cargohold"));
				cargo.RecordHistory(new(time, $"Removed from ship {Name}({Id})"));
			}
			return true;
		}

        public void RecordHistory(LogEntry entry) => Log.Add(entry);

        public override string ToString() => $"Ship - {Name}({Id})";
    }
}
