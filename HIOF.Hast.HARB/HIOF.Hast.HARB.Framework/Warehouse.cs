namespace HIOF.Hast.HARB.Framework
{
    /// <summary>Represents a warehouse for holding cargo.</summary>
    /// <param name="name">Name of warehouse.</param> 
    /// <param name="capacity">Amount of cargo-objects the warehouse can hold.</param>
    public class Warehouse(string name, int capacity)
    {
        private static int idCount = 0;
		public int Id { get; } = idCount++;
		public string Name { get; set; } = name;

        private int _maxCapacity = capacity;
        public int MaxCapacity
        {
            get => _maxCapacity;
            set
            {
                if (Inventory.Count == 0) _maxCapacity = value;
                else throw new Exception("Inventory is not empty.");
            }
        }
        public HashSet<Cargo> Inventory { get; } = [];

		internal bool AddCargo(Cargo cargo)
		{
            if (Inventory.Count < MaxCapacity)
			    return Inventory.Add(cargo);
            return false;
		}

		internal bool AddCargo(Cargo cargo, DateTime time)
        {
            if (Inventory.Count < MaxCapacity)
            {
                cargo.RecordHistory(new(time, $"Added to warehouse {Name}({Id})"));
                return Inventory.Add(cargo);
            }
			return false;
        }

		internal bool RemoveCargo(Cargo cargo)
		{
			return Inventory.Remove(cargo);
		}

		internal bool RemoveCargo(Cargo cargo, DateTime time)
        {
            cargo.RecordHistory(new(time, $"Removed from warehouse {Name}({Id})"));
            return Inventory.Remove(cargo);
        }

        public override string ToString() => $"Cargo - {Name}({Id}) - {Inventory.Count} / {MaxCapacity}";
    }
}
