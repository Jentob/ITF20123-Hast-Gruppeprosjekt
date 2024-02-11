namespace HIOF.Hast.HARB.Framework
{
    /// <summary>Represents a port for ships to dock to Can be used to load/unload cargo.</summary>
    /// <param name="name">Name of port.</param> 
    /// <param name="size">Represents maximum size of ship able to dock.</param>
    public class Port(string name, ShipSize size)
    {
        private static int idCount = 0;
		public int Id { get; } = idCount++;
        public string Name { get; } = name;
        public ShipSize Size { get; } = size;
        public Ship? OccupyingShip { get; private set; }

        private bool CanShipDock(Ship ship)
        {
            if(OccupyingShip == null && ship.Size <= Size)
                return true;
            return false;
        }

        public bool OccupyPort(Ship ship)
        {
            if (CanShipDock(ship))
            {
                OccupyingShip = ship;
                return true;
            }
            return false;
        }

		public bool OccupyPort(Ship ship, DateTime time)
        {
			if (CanShipDock(ship))
			{
				ship.RecordHistory(new(time, $"Docked at {Name}({Id})"));
				OccupyingShip = ship;
				return true;
			}
			return false;
		}

		public Ship? LeavePort()
		{
			if (OccupyingShip != null)
			{
				Ship? shipToLeave = OccupyingShip;
				OccupyingShip = null;
				return shipToLeave;
			}
			return null;
		}

		public Ship? LeavePort(DateTime time)
        {
            if (OccupyingShip != null) {
                Ship? shipToLeave = OccupyingShip;
                OccupyingShip = null; 
                shipToLeave.RecordHistory(new(time, $"Undocked at {Name}({Id})"));
                return shipToLeave;
            }
            return null;
        }

		public bool AddCargo(Cargo cargo)
		{
			if (OccupyingShip != null && OccupyingShip.AddCargo(cargo)) return true;
			return false;
		}

		public bool AddCargo(Cargo cargo, DateTime time)
        {
            if (OccupyingShip != null && OccupyingShip.AddCargo(cargo, time)) return true;
            return false;
        }

		public Cargo? RemoveCargo(Cargo cargo)
		{
			if (OccupyingShip != null) return OccupyingShip.RemoveCargo(cargo);
			return null;
		}

		public Cargo? RemoveCargo(Cargo cargo, DateTime time)
		{
			if (OccupyingShip != null) return OccupyingShip.RemoveCargo(cargo, time);
            return null;
		}

        public override string ToString()
        {
            string str = $"{GetType().Name} - {Name}({Id}) - Max size: {Size}";
            if (OccupyingShip != null) return str + $" - Occupied by {OccupyingShip}";
            else return str + " - Not currently occupied";
        }
    }
}