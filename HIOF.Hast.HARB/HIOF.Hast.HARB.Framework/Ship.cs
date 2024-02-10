namespace HIOF.Hast.HARB.Framework
{
	/// <summary>Represents an arbritary ship size.</summary>
	public enum ShipSize
	{
		Small,
		Medium,
		Large,
	}
	/// <summary>LogEntry keeps time and event separated.</summary>
	public struct LogEntry(DateTime _time, string _message)
	{
		public DateTime time = _time;
		public string message = _message;
		
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
		public HashSet<Cargo> Cargohold { get; } = [];
		public int MaxCargoWeightInKG { get; } = maxCarryWeightInKG;
        public List<SailingSchedule> SailingSchedules { get; } = [];



        public void AddToSailingSchedule(DateTime departureTime, TimeSpan interval)
        {
            SailingSchedules.Add(new SailingSchedule(departureTime, interval));
        }

		private bool CargoCheck(Cargo cargo)
		{
			double weight = cargo.WeightInKG;
			foreach (Cargo item in Cargohold)
				weight += item.WeightInKG;
			if (weight < MaxCargoWeightInKG)
				return true;
			return false;
		}

		/// <summary>Adds cargo to the cargohold</summary>
		/// <param name="cargo">The cargo to be loaded onboard the ship</param>
		internal bool AddCargo(Cargo cargo)
		{
			if(!CargoCheck(cargo))
				return false;

			Cargohold.Add(cargo);

			return true;
		}

		internal bool AddCargo(Cargo cargo, DateTime time)
		{
			if (!CargoCheck(cargo))
				return false;

			cargo.RecordHistory(new(time, $"Added to ship {Name}({Id})"));
			RecordHistory(new(time, $"{cargo.Name}({cargo.Id}) added to cargohold"));
		
			Cargohold.Add(cargo);

			return true;
		}

		/// <summary>Remove cargo from the ship object.</summary>
		/// <param name="cargo"></param>
		/// <returns>Returns the cargo interface.</returns>
		internal bool RemoveCargo(Cargo cargo)
		{
			if (!Cargohold.Contains(cargo)) return false;
			Cargohold.Remove(cargo);
			
			return true;
		}

		internal bool RemoveCargo(Cargo cargo, DateTime time)
		{
			if (!Cargohold.Contains(cargo)) return false;
			Cargohold.Remove(cargo);
						
			RecordHistory(new(time, $"{cargo.Name}({cargo.Id}) removed from cargohold"));
			cargo.RecordHistory(new(time, $"Removed from ship {Name}({Id})"));
			
			return true;
		}

        internal void RecordHistory(LogEntry entry) => Log.Add(entry);

        public override string ToString() => $"Ship - {Name}({Id})";
    }
}
