using System.ComponentModel;

namespace HIOF.Hast.HARB.Framework
{
    /// <summary>Represents a harbor</summary>
    public class Harbor(string name)
    {
		private static int idCount = 0;
		public int Id { get; } = idCount++;
		public string Name { get; set; } = name;
        /// <summary>Represents a queue for ships wanting to dock.</summary>
        public List<Ship> WaitingQueue { get; } = [];
        public List<Ship> SailingShips { get; } = [];
        public List<Warehouse> Warehouses { get; } = [];
        public List<Port> Ports { get; } = [];
      
        public void AddWarehouse(Warehouse warehouse)
        {
            Warehouses.Add(warehouse);
        }

		public void AddPort(Port port)
		{
			Ports.Add(port);
            Ports.Sort((p1, p2) => p1.Size.CompareTo(p2.Size));
		}

        public void AddShip(Ship ship)
        {
            WaitingQueue.Add(ship);
        }

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
                        ship.RecordHistory(new(time, $"Docked at {port.Name}({port.Id})"));
						port.OccupyPort(ship);
                        WaitingQueue.Remove(ship);
						if (!ArePortsAvailable())
							return;
						break;
					}
				}
			}
		}

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
        
        public void LoadCargoToShips(DateTime time)
        {
            foreach (Warehouse warehouse in Warehouses)
            {
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

		public void ReleaseShips(DateTime time)
        {
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
