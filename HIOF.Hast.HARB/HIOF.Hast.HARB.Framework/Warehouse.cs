using System.Collections.ObjectModel;

namespace HIOF.Hast.HARB.Framework
{
    /// <summary>
    /// Represents a warehouse for holding cargo.
    /// </summary>
    /// <param name="name">Name of warehouse.</param> 
    /// <param name="capacity">Amount of cargo-objects the warehouse can hold.</param>
    public class Warehouse(string name, int capacity)
    {
		/// <summary>Used to auto increment <see cref="Id"/>.</summary>
        private static int idCount = 0;
		/// <summary>Represents a unique identifier for each warehouse-object.</summary>
		public int Id { get; } = idCount++;
		/// <summary>Name of warehouse.</summary>
		public string Name { get; set; } = name;
        /// <summary>The amount of cargo-objects a warehouse can hold.</summary>
        private int _maxCapacity = capacity;
        /// <summary>The amount of cargo-objects a warehouse can hold.</summary>
        public int MaxCapacity
        {
            get => _maxCapacity;
            set
            {
                if (Inventory.Count == 0) _maxCapacity = value;
                else throw new Exception("Inventory is not empty.");
            }
        }
        /// <summary>The items stored in the warehouse.</summary>
        internal HashSet<Cargo> Inventory { get; } = [];

        /// <summary>
		/// Retrieves a collection of the cargo stored by the warehouse.
		/// </summary>
		/// <returns>A collection containing cargo.</returns>
        public Collection<Cargo> GetInventory()
		{
			return [.. Inventory];
		}

        /// <summary>
		/// Checks if the warehouse is full.
		/// </summary>
		/// <returns><c>true</c> if the warehouse is full. <c>false</c> if it isn't.</returns>
        internal bool IsWarehouseFull()
        {
            return Inventory.Count >= MaxCapacity;
        }

        /// <summary>
		/// Adds cargo to the <see cref="Inventory"/>.
		/// </summary>
		/// <param name="cargo">The cargo to be added.</param>
		/// <returns><c>true</c> on success. <c>false</c> on fail.</returns>
		internal bool AddCargo(Cargo cargo)
		{
            if (IsWarehouseFull())
                return false;
			return Inventory.Add(cargo);
		}

        /// <summary>
		/// Works the same as <see cref="AddCargo(Cargo)"/> but also logs.
		/// </summary>
		/// <param name="cargo">The cargo to be added.</param>
		/// <param name="time">Used for logging.</param>
		/// <returns><c>true</c> on success. <c>false</c> on fail.</returns>
		internal bool AddCargo(Cargo cargo, DateTime time)
        {
            if (IsWarehouseFull()) 
			    return false;
            cargo.RecordHistory(new(time, $"Added to warehouse {Name}({Id})"));
            return Inventory.Add(cargo);
        }

        /// <summary>
		/// Removes cargo from the <see cref="Inventory"/>.
		/// </summary>
		/// <param name="cargo">The cargo to be removed.</param>
		/// <returns>The cargo on success. null on fail.</returns>
		internal Cargo? RemoveCargo(Cargo cargo)
		{
            if(!Inventory.Remove(cargo))
                return null;
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
            if(!Inventory.Remove(cargo))
                return null;
            cargo.RecordHistory(new(time, $"Removed from warehouse {Name}({Id})"));
			return cargo;
		}

        public override string ToString() => $"{GetType().Name} - {Name}({Id}) - {Inventory.Count} / {MaxCapacity} items";
    }
}
