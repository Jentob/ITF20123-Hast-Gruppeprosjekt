namespace HIOF.Hast.HARB.Framework
{
    /// <summary>Represents a harbor</summary>
    public class Harbor
    {
        /// <summary>Represents a queue for ships wanting to dock.</summary>
        public List<Ship> WaitingQueueShips { get; } = [];
        public List<Port> Port { get; } = [];

      

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
            
                Ship? ship = CreateShip(i + 1);
                if (ship != null)
                {
                    WaitingQueueShips.add(ship);

                    Console.WriteLine($"The ship {ship.Name} has been added to the harbor.");
                }
                
            }
        }

    /// <summary>Used by ConfigureHarbor() to create ships.</summary>
    private static Ship? CreateShip(int index)
    {
        Console.Write($"Enter the name of ship {index}: ");
        string? shipName = Console.ReadLine();

        Console.Write($"Choose the size of the ship (Small/Medium/Large) for {shipName}: ");
        ShipSize? shipSize = Enum.Parse<ShipSize>(Console.ReadLine(), true);
        switch (shipSize)
        {
        case "Small";
            ship.maxCarryWeightInTons == 100;
            break;
        case "Medium";
            ship.maxCarryWeightInTons == 500;
            break;
        case "Large";
            ship.maxCarryWeightInTons == 100;
        default:
            Console.WriteLine("");
        } 
    }

    }

        Console.Write($"Do you want recurring sailings for {shipName}? (Yes/No): ");
            bool? hasRecurringSailings = Console.ReadLine().Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);

            if (shipName == null || shipSize == null || hasRecurringSailings == null) {
                return null;
            }

            Ship ship = new Ship(shipName, (ShipSize)shipSize, 1000);

            // TODO: Add logic for recurring sailings
            if ((bool)hasRecurringSailings)
            {
              
            }
            return ship;
        }
    }
}
