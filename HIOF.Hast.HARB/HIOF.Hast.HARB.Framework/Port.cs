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

        /// <summary>
        /// Used for checking if a ship can dock.
        /// </summary>
        /// <param name="ship">The ship to check with.</param>
        /// <returns><c>true</c> if it can. <c>false</c> if it can not.</returns>
        private bool CanShipDock(Ship ship)
        {
            if(OccupyingShip == null && ship.Size <= Size)
                return true;
            return false;
        }

        /// <summary>
        /// Docks the ship.
        /// </summary>
        /// <param name="ship">The ship to dock.</param>
        /// <returns><c>true</c> if it docked successfully. <c>false</c> if it fails.</returns>
        internal bool OccupyPort(Ship ship)
        {
            if (CanShipDock(ship))
            {
                OccupyingShip = ship;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Works the same as <see cref="OccupyPort(Ship) "/> but also logs.
        /// </summary>
        /// <param name="ship">The ship to dock.</param>
        /// <param name="time">Used for logging.</param>
        /// <returns><c>true</c> if it docked successfully. <c>false</c> if it fails.</returns>
        internal bool OccupyPort(Ship ship, DateTime time)
        {
            if (CanShipDock(ship))
            {
                ship.RecordHistory(new(time, $"Docked at {Name}({Id})"));
                OccupyingShip = ship;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Undocks the ship from the port.
        /// </summary>
        /// <returns>The ship on success, else returns null.</returns>
        internal Ship? LeavePort()
        {
            if (OccupyingShip != null)
            {
                Ship? shipToLeave = OccupyingShip;
                OccupyingShip = null;
                return shipToLeave;
            }
            return null;
        }

        /// <summary>
        /// Works the same as <see cref="LeavePort() "/> but also logs.
        /// </summary>
        /// <param name="time">Used for logging.</param>
        /// <returns>The ship on success, else returns null.</returns>
        internal Ship? LeavePort(DateTime time)
        {
            if (OccupyingShip != null) {
                Ship? shipToLeave = OccupyingShip;
                OccupyingShip = null; 
                shipToLeave.RecordHistory(new(time, $"Undocked at {Name}({Id})"));
                return shipToLeave;
            }
            return null;
        }

        /// <summary>
        /// Loads cargo onto the docked ship.
        /// </summary>
        /// <param name="cargo">The cargo to load onboard.</param>
        /// <returns><c>true</c> on success. <c>false</c> if it fails.</returns>
        internal bool AddCargo(Cargo cargo)
        {
            if (OccupyingShip != null && OccupyingShip.AddCargo(cargo)) return true;
            return false;
        }

        /// <summary>
        /// Works the same as <see cref="AddCargo(Cargo) "/> but also logs.
        /// </summary>
        /// <param name="cargo">The cargo to load onboard.</param>
        /// <param name="time">Used for logging.</param>
        /// <returns><c>true</c> on success. <c>false</c> if it fails.</returns>
        internal bool AddCargo(Cargo cargo, DateTime time)
        {
            if (OccupyingShip != null && OccupyingShip.AddCargo(cargo, time)) return true;
            return false;
        }

        /// <summary>
        /// Loads cargo off the docked ship.
        /// </summary>
        /// <param name="cargo">The cargo to remove.</param>
        /// <returns>The cargo on success, else returns null.</returns>
        internal Cargo? RemoveCargo(Cargo cargo)
        {
            if (OccupyingShip != null) return OccupyingShip.RemoveCargo(cargo);
            return null;
        }

        /// <summary>
        /// Works the same as <see cref="RemoveCargo(Cargo) "/> but also logs.
        /// </summary>
        /// <param name="cargo">The cargo to remove.</param>
        /// <param name="time">Used for logging.</param>
        /// <returns>The cargo on success, else returns null.</returns>
        internal Cargo? RemoveCargo(Cargo cargo, DateTime time)
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