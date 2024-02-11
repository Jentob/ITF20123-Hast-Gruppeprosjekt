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

        public void DockShips(DateTime time)
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
                        ship.RecordHistory(new(time, $"Docked at {Name}({Id})"));
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
                        if (c != null)
                            if(!warehouse.AddCargo(c, time))
                                break;
                    }
                }
            }     
        }
        
        // TODO: Metode for å flytte last på skip. Samme implementasjon som OffloadCargoFromShips men fra Warehouse til Ship
        // TODO: Metode for å sende skip fra havnen hvis di har en seiling
        // TODO: Metode for å flytte skip fra seilinger inn i WaitingQueue
        // TODO: Fikse et system for seilinger

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
