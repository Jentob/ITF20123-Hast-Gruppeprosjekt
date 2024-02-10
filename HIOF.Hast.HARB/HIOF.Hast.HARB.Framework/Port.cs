using System.Reflection.Metadata.Ecma335;

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

        public bool OccupyPort(Ship ship, DateTime? time = null)
        {
            if (OccupyingShip == null && ship != null && ship.Size <= Size) {
                if(time != null) ship.RecordHistory(new(time, $"Docked at {Name}({Id})"));
                OccupyingShip = ship;
                return true;
            }
            return false;
        }

        public Ship? LeavePort(DateTime? time = null)
        {
            if (OccupyingShip != null) {
                Ship? shipToLeave = OccupyingShip;
                OccupyingShip = null; 
                if(time != null) shipToLeave.RecordHistory(new(time, $"Undocked at {Name}({Id})"));
                return shipToLeave;
            }
            return null;
        }

        public bool AddCargo(Cargo cargo, DateTime? time = null)
        {
            if (OccupyingShip != null && OccupyingShip.AddCargo(cargo, time)) return true;
            return false;
        }

        public bool RemoveCargo(ICargo cargo, DateTime? time = null)
		{
			if (OccupyingShip != null && OccupyingShip.RemoveCargo(cargo, time)) return true;
            return false;
		}

        public override string ToString()
        {
            if (OccupyingShip != null) return $"Port - {Name}({Id}) - Occupied by {OccupyingShip})";
            else return $"Port - {Name}({Id}) - Not currently Occupied";
        }
    }
}