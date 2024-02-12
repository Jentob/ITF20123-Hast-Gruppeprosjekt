using System.Collections.ObjectModel;

namespace HIOF.Hast.HARB.Framework
{
	/// <summary>Represents an arbritary ship size.</summary>
	public enum ShipSize
	{
		Small,
		Medium,
		Large,
	}
	/// <summary>Indicates if a sailing is recurring.</summary>
	public enum RecurringSailing
	{
		None,
		Daily,
		Weekly,
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
    /// <param name="maxCargoWeightInTons">Represents max cargo weight the ship is able to handle.</param>
    public class Ship(string name, ShipSize size, double maxCargoWeightInTons, DateTime? sailingDate = null, string destination = "UNKNOWN", int triplength = 1, RecurringSailing recurringSailing = RecurringSailing.Weekly)
	{
		private static int idCount = 0;
		public int Id { get; } =idCount++;
		public string Name { get; set; } = name;
		public ShipSize Size { get; } = size;
		internal List<LogEntry> Log { get; } = [];
		internal HashSet<Cargo> Cargohold { get; } = [];
		public double MaxCargoWeightInTons { get; } = maxCargoWeightInTons;

		public DateTime? SailingDate { get; set; } = sailingDate;
		public int TripLength { get; set; } = triplength;
        public RecurringSailing Recurring { get; set; } = recurringSailing;
		public string Destination { get; set; }= destination;

		/// <summary>
		/// Retrieves the history of the ship.
		/// </summary>
		/// <returns>A copy of <see cref="Log"/>.</returns>
		public IList<LogEntry> GetLog()
		{
			return [.. Log];
		}

		/// <summary>
		/// Retrieves a collection of the cargo stored by the ship.
		/// </summary>
		/// <returns>A collection containing cargo.</returns>
		public Collection<Cargo> GetCargohold()
		{
			return [.. Cargohold];
		}

		/// <summary>
		/// Caclulates the total weight of the cargo onboard.
		/// </summary>
		/// <returns>The total weight.</returns>
		public double CargoWeight() {
			double weight = 0;
			foreach (Cargo item in Cargohold)
				weight += item.WeightInTons;
			return weight;
		}

		/// <summary>
		/// Checks if a cargo-object can be added to the <see cref="Cargohold"/>.
		/// </summary>
		/// <param name="cargo">Cargo to be checked.</param>
		/// <returns><c>true</c> if the cargo can be added. <c>false</c> if it can't.</returns>
		private bool CargoCheck(Cargo cargo)
		{
			double weight = cargo.WeightInTons + CargoWeight();
			if (weight <= MaxCargoWeightInTons)
				return true;
			return false;
		}

		/// <summary>
		/// Adds cargo to the <see cref="Cargohold"/>.
		/// </summary>
		/// <param name="cargo">The cargo to be loaded onboard the ship.</param>
		/// <returns><c>true</c> on success. <c>false</c> on fail.</returns>
		internal bool AddCargo(Cargo cargo)
		{
			if (!CargoCheck(cargo))
				return false;

			Cargohold.Add(cargo);

			return true;
		}

		/// <summary>
		/// Works the same as <see cref="AddCargo(Cargo)"/> but also logs.
		/// </summary>
		/// <param name="cargo">The cargo to be loaded onboard the ship.</param>
		/// <param name="time">Used for logging.</param>
		/// <returns><c>true</c> on success. <c>false</c> on fail.</returns>
		internal bool AddCargo(Cargo cargo, DateTime time)
		{
			if (!CargoCheck(cargo))
				return false;

			cargo.RecordHistory(new(time, $"Added to ship {Name}({Id})"));
			RecordHistory(new(time, $"{cargo.Name}({cargo.Id}) added to cargohold"));

			Cargohold.Add(cargo);

			return true;
		}

		/// <summary>
		/// Removes cargo from the <see cref="Cargohold"/>.
		/// </summary>
		/// <param name="cargo">The cargo to be removed.</param>
		/// <returns>The cargo on success. null on fail.</returns>
		internal Cargo? RemoveCargo(Cargo cargo)
		{
			if (!Cargohold.Contains(cargo)) return null;
			Cargohold.Remove(cargo);

			return cargo;
		}

		/// <summary>
		/// Works the same as <see cref="RemoveCargo(Cargo)"/> but also logs.
		/// </summary>
		/// <param name="cargo">The cargo to be removed.</param>
		/// <param name="time">Used for logging.</param>
		/// <returns>The cargo on success. null on fail.</returns>
		internal Cargo? RemoveCargo(Cargo cargo, DateTime time)
		{
			if (!Cargohold.Contains(cargo)) return null;
			Cargohold.Remove(cargo);

			RecordHistory(new(time, $"{cargo.Name}({cargo.Id}) removed from cargohold"));
			cargo.RecordHistory(new(time, $"Removed from ship {Name}({Id})"));

			return cargo;
		}

		/// <summary>
		/// Adds a record to <see cref="Log"/>.
		/// </summary>
		/// <param name="entry"><see cref="LogEntry"/> to be added.</param>
		internal void RecordHistory(LogEntry entry) => Log.Add(entry);

		public override string ToString() => $"{GetType().Name} - {Name}({Id}) - Size: {Size} - Cargo: {Math.Round(CargoWeight(), 2)} / {MaxCargoWeightInTons} metric tons";
	}
}