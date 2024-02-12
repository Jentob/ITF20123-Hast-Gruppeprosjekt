using System.Collections.ObjectModel;

namespace HIOF.Hast.HARB.Framework
{
    /// <summary>Represents a harbor</summary>
    public class Harbor(string name)
    {
		private static int idCount = 0;
		public int Id { get; } = idCount++;
		public string Name { get; set; } = name;
        internal List<Ship> WaitingQueue { get; } = [];
        internal List<Ship> SailingShips { get; } = [];
        internal List<Warehouse> Warehouses { get; } = [];
        internal List<Port> Ports { get; } = [];

        /// <summary>
        /// Retrieves a copy of ships waiting to dock.
        /// </summary>
        /// <returns>A list of the queue.</returns>
        public IList<Ship> GetWaitingQueue()
		{
			return [.. WaitingQueue];
		}

        /// <summary>
        /// Retrieves a copy of ships sailing.
        /// </summary>
        /// <returns>A collection of ships sailing.</returns>
        public Collection<Ship> GetSailingShips()
		{
			return [.. SailingShips];
		}

        /// <summary>
        /// Retrieves a copy of warehouses.
        /// </summary>
        /// <returns>A collection of the warehouses.</returns>
        public Collection<Warehouse> GetWarehouses()
		{
			return [.. Warehouses];
		}

        /// <summary>
        /// Retrieves a copy of ports.
        /// </summary>
        /// <returns>A collection of the ports.</returns>
        public Collection<Port> GetPorts()
		{
			return [.. Ports];
		}

        /// <summary>
        /// Used when setting up the harbor to add a warehouse.
        /// </summary>
        /// <param name="warehouse">The warehouse to be added to <see cref="Warehouses"/>.</param>
        public void AddWarehouse(Warehouse warehouse)
        {
            Warehouses.Add(warehouse);
        }

        /// <summary>
        /// Used when setting up the harbor to add a port.
        /// </summary>
        /// <param name="port">The port to be added to <see cref="Ports"/>.</param>
		public void AddPort(Port port)
		{
			Ports.Add(port);
            Ports.Sort((p1, p2) => p1.Size.CompareTo(p2.Size));
		}
        
        /// <summary>
        /// Used when setting up the harbor to add a ship.
        /// </summary>
        /// <remarks>Ships are added to <see cref="WaitingQueue"/>.</remarks>
        /// <param name="ship">The ship to be added to <see cref="WaitingQueue"/>.</param>
        public void AddShip(Ship ship)
        {
            WaitingQueue.Add(ship);
        }

        /// <summary>
        /// Used for checking if a port is available.
        /// </summary>
        /// <returns><c>true</c> if a port is empty. <c>false</c> if ports are full.</returns>
        private bool ArePortsAvailable()
        {
            foreach (Port port in Ports)
            {
                foreach (Ship ship in WaitingQueue)
                {
					if (port.OccupyingShip == null && port.Size >= ship.Size)
						return true;
				}
            }
            return false;
        }
        
        /// <summary>
        /// Used when setting up the harbor to add cargo.
        /// </summary>
        /// <remarks>Cargo are added to the warehouses in <see cref="Warehouses"/>.</remarks>
        /// <param name="cargo">The cargo to be added to a warehouse in <see cref="Warehouses"/>.</param>
        public void AddCargo(Cargo cargo)
        {
            foreach (Warehouse warehouse in Warehouses)
            {
                if (warehouse.IsWarehouseFull())
                    continue;
                if(warehouse.AddCargo(cargo))
                    break;
            }
        }

        /// <summary>
        /// Moves ships from <see cref="WaitingQueue"/> to a port in <see cref="Ports"/> if they fit.
        /// </summary>
        // TODO: Optimaliser
		public void DockShips()
        {
			if (!ArePortsAvailable() || WaitingQueue.Count < 1)
				return;

			Ship[] shipsToDock = [.. WaitingQueue]; 

			foreach (Ship ship in shipsToDock)
			{
				foreach (Port port in Ports)
				{
					if (port.OccupyingShip == null && port.Size >= ship.Size)
					{
						port.OccupyPort(ship);
                        WaitingQueue.Remove(ship);
						if (!ArePortsAvailable())
							return;
						break;
					}
				}
			}
		}

        /// <summary>
        /// Works the same as <see cref="DockShips() "/> but also logs.
        /// </summary>
        /// <param name="time">Used for logging.</param>
        internal void DockShips(DateTime time)
        {
			if (!ArePortsAvailable() || WaitingQueue.Count < 1)
				return;

			Ship[] shipsToDock = [.. WaitingQueue]; 

			foreach (Ship ship in shipsToDock)
			{
				foreach (Port port in Ports)
				{
					if (port.OccupyingShip == null && port.Size >= ship.Size)
					{
						port.OccupyPort(ship, time);
                        WaitingQueue.Remove(ship);
						if (!ArePortsAvailable())
							return;
						break;
					}
				}
			}
		}

        /// <summary>
        /// Moves cargo from ships docked to warehouses.
        /// </summary>
        /// <param name="time">Used for logging.</param>
        public void OffloadCargoFromShips(DateTime time)
        {
            foreach (Port port in Ports)
            {
                if (port.OccupyingShip == null)
                    continue;

                foreach (Warehouse warehouse in Warehouses)
                {
                    if (warehouse.IsWarehouseFull())
                        continue;
                    foreach (Cargo cargo in port.OccupyingShip.Cargohold)
                    {
                        Cargo? c = port.OccupyingShip.RemoveCargo(cargo, time);
                        if (c != null && !warehouse.AddCargo(c, time))
                            break;
                    }
                }
            }
        }
        
        /// <summary>
        /// Moves cargo from warehouses to ships docked.
        /// </summary>
        /// <param name="time">Used for logging.</param>
        public void LoadCargoToShips(DateTime time)
        {
            foreach (Warehouse warehouse in Warehouses)
            {
                // TODO: Må endres
                foreach (Cargo c in warehouse.Inventory)
                {
                    Cargo? cargo = warehouse.RemoveCargo(c, time);
                    if (cargo == null)
                        continue;
                    bool isFull = true;
                    foreach (Port port in Ports)
                    {
                        if (port.OccupyingShip == null)
                        continue;
                        if (port.OccupyingShip.AddCargo(cargo, time))
                        {
                            isFull = false;
                            break;
                        }
                    }
                    if (isFull)
                    {
                        warehouse.AddCargo(cargo, time);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Moves ships from <see cref="Ports"/> to <see cref="SailingShips"/>.
        /// </summary>
        /// <param name="time">Used to for logging and for figuring out if a ship should leave.</param>
		public void ReleaseShips(DateTime time)
        {
            // TODO: Sjekke om et skip har vært for lenge til kai og flytte det til WaitingQueue
            foreach (Port port in Ports)
            {
                if (port.OccupyingShip != null && port.OccupyingShip.SailingDate != null && port.OccupyingShip.SailingDate <= time)
                {
                    Ship? leavingShip = port.LeavePort(time);
                    // Eneste grunnen for denne sjekken er fordi IDEen klager
                    // LeavePort() vil ikke returnere null her
                    if (leavingShip == null)
                        continue;
                    leavingShip.RecordHistory(new(time, $"Sailing to {leavingShip.Destination}"));
                    SailingShips.Add(leavingShip);
                }
            }
        }

        /// <summary>
        /// Moves ships from <see cref="SailingShips"/> to <see cref="WaitingQueue"/>.
        /// </summary>
        /// <param name="time">Used to for logging and for figuring out if a ship should return.</param>
		public void QueueShips(DateTime time)
        {
            Ship[] shipsToQueue = [.. SailingShips];
            foreach (Ship ship in shipsToQueue)
            {
                if (ship.SailingDate != null && ((DateTime)ship.SailingDate).AddDays(ship.TripLength) <= time)
                {
                    ship.RecordHistory(new(time, $"Returned from {ship.Destination}"));

                    switch (ship.Recurring)
                    {
                        case RecurringSailing.None:
                            ship.SailingDate = null;
                            break;
                        case RecurringSailing.Daily:
                            ship.SailingDate = ((DateTime)ship.SailingDate).AddDays(1);
                            break;
                        case RecurringSailing.Weekly:
                            ship.SailingDate = ((DateTime)ship.SailingDate).AddDays(7);
                            break;
                        default:
                        throw new Exception();
                    };
                    if(SailingShips.Remove(ship))
                        WaitingQueue.Add(ship);
                }
            }
        }
        
        /// <summary>
        /// Collects all ships from all internal lists.
        /// </summary>
        /// <returns>Every existing ship in the harbor-obejct.</returns>
        public IList<Ship> GetAllShips()
        {
            List<Ship> ships = [];
            foreach (Port port in Ports)
            {
                if (port.OccupyingShip != null)
                {
                    ships.Add(port.OccupyingShip);
                }
            }
            ships.AddRange(WaitingQueue);
            ships.AddRange(SailingShips);

            return ships;
        }

        /// <summary>
        /// Collects all cargo from all internal lists.
        /// </summary>
        /// <returns>Every existing cargo-object in the harbor-obejct.</returns>
        public List<Cargo> GetAllCargo()
        {
            List<Cargo> cargoList = [];
            foreach (Warehouse warehouse in Warehouses)
            {
                foreach (Cargo cargo in warehouse.Inventory)
                {
                    cargoList.Add(cargo);
                }
            }
            foreach (Ship ship in GetAllShips())
            {
                foreach (Cargo cargo in ship.Cargohold)
                {
                    cargoList.Add(cargo);
                }
            }

            return cargoList;
        }

        /// <summary>
        /// Retrieves a ship by it's <see cref="Ship.Id"/>.
        /// </summary>
        /// <param name="id">The id to find.</param>
        /// <returns>The ship if it exists, else returns null.</returns>
        public Ship? GetShipById(int id)
        {
            return GetAllShips().FirstOrDefault(ship => ship.Id == id);
        }

        /// <summary>
        /// Retrieves a ship by it's <see cref="Ship.Name"/>.
        /// </summary>
        /// <param name="name">The name to find.</param>
        /// <returns>The ship if it exists, else returns null.</returns>
        public Ship? GetShipByName(string name)
        {
            return GetAllShips().FirstOrDefault(ship => ship.Name == name);
        }
        
        /// <summary>
        /// Retrieves a cargo-object by it's <see cref="Cargo.Id"/>.
        /// </summary>
        /// <param name="id">The id to find.</param>
        /// <returns>The cargo if it exists, else returns null.</returns>
        public Cargo? GetCargoById(int id)
        {
            return GetAllCargo().FirstOrDefault(cargo => cargo.Id == id);
        }

        /// <summary>
        /// Retrieves a cargo-object by it's <see cref="Cargo.Name"/>.
        /// </summary>
        /// <param name="name">The name to find.</param>
        /// <returns>The cargo if it exists, else returns null.</returns>
        public Cargo? GetCargoByName(string name)
        {
            return GetAllCargo().FirstOrDefault(cargo => cargo.Name == name);
        }



		/// <summary>CLI to configure a harbor.</summary>
		public void ConfigureHarbor()
        {
            Console.WriteLine("Welcome to our harbor simulator");
            
            Console.Write("Enter the number of ships you want to add for simulation: ");

            int numberOfShips;

            while (!int.TryParse(Console.ReadLine(), out numberOfShips) || numberOfShips <= 0)
            {
                Console.Write("Invalid number, please enter a positive integer: ");
            }
    
            for (int i = 0; i < numberOfShips; i++)
            {
                Ship? ship = CliCreateShip(i + 1);
                if (ship != null)
                {
                    WaitingQueue.Add(ship);

                    Console.WriteLine($"The ship {ship.Name} has been added to the harbor.");
                }
                
            }
        }

        /// <summary>Used by ConfigureHarbor() to create ships.</summary>
        private static Ship? CliCreateShip(int index)
        {
			Console.Write($"Enter the name of ship {index}: ");
			string? shipName = Console.ReadLine();

			Console.Write($"Choose the size of the ship (Small/Medium/Large) for {shipName}: ");
            ShipSize? shipSize = null;
            double maxCarryWeight;
            while (true)
            {
                try
                {
                    string? tmp = Console.ReadLine();
                    if (tmp != null)
                    shipSize = Enum.Parse<ShipSize>(tmp, true);

                    maxCarryWeight = shipSize switch
                    {
                        ShipSize.Small => 2,
                        ShipSize.Medium => 40,
                        ShipSize.Large => 500,
                        _ => throw new Exception(),
                    };
                    break;
            }
                catch (Exception)
                {
                    continue;
                }
            }

			Console.Write($"Do you want recurring sailings for {shipName}? (Yes/No): ");
            string? tmp2 = Console.ReadLine();
            bool? hasRecurringSailings = null;
            if (tmp2 != null)
			    hasRecurringSailings = tmp2.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);

			if (shipName == null || shipSize == null || hasRecurringSailings == null)
			{
				return null;
			}

			Ship ship = new Ship(shipName, (ShipSize)shipSize, maxCarryWeight);

			// TODO: Add logic for recurring sailings
			if ((bool)hasRecurringSailings)
			{

			}
			return ship;
		}

		public override string ToString()
		{
			return $"Harbor - {Name}({Id}) - Holds {Warehouses.Count} warehouses, {Ports.Count} ports";
		}
	}
}
