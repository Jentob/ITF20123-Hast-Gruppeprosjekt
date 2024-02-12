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

        internal bool IsWarehouseFull()
        {
            return Inventory.Count >= MaxCapacity;
        }

		internal bool AddCargo(Cargo cargo)
		{
            if (IsWarehouseFull())
                return false;
			return Inventory.Add(cargo);
		}

		internal bool AddCargo(Cargo cargo, DateTime time)
        {
            if (IsWarehouseFull()) 
			    return false;
            cargo.RecordHistory(new(time, $"Added to warehouse {Name}({Id})"));
            return Inventory.Add(cargo);
        }

		internal Cargo? RemoveCargo(Cargo cargo)
		{
            if(!Inventory.Remove(cargo))
                return null;
            return cargo;
		}

		internal Cargo? RemoveCargo(Cargo cargo, DateTime time)
        {
            if(!Inventory.Remove(cargo))
                return null;
            cargo.RecordHistory(new(time, $"Removed from warehouse {Name}({Id})"));
			return cargo;
		}

        public override string ToString() => $"{GetType().Name} - {Name}({Id}) - {Inventory.Count} / {MaxCapacity} items";
    }
}
