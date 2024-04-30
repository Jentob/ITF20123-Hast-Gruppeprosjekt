using HIOF.Hast.HARB.Framework;

namespace HIOF.Hast.HARB.FrameworkImplementation
{
    internal class Program
    {

        static void Main(string[] args)
        {
            StartSimulationWithRandomObjects(args);
        }

        static void StartSimulation(Harbor harbor)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddYears(1);

            StartSimulation(start, end, harbor);

        }

        static void StartSimulation(DateTime end, Harbor harbor)
        {
            DateTime start = DateTime.Now;

            StartSimulation(start, end, harbor);

        }

        static void StartSimulation(DateTime start, DateTime end, Harbor harbor)
        {
            Console.WriteLine("Simulation started");

            SimulationDriver.Run(harbor, start, end);
            Console.WriteLine("Simulation ended");
        }

        static void StartSimulationWithRandomObjects(string[] args)
        {

            Harbor harbor = new("Harbor 1");
            DateTime start = DateTime.Now;


            // -----------------
            // Oppsett av havnen
            // -----------------
            harbor.AddWarehouse(new("Large warehouse", 20));
            harbor.AddWarehouse(new("Small warehouse", 10));

            List<Cargo> cargoList = [];

            int amountOfItems = 15;
            Random random = new();
            double min = 0.1;
            double max = 5;
            for (int i = 0; i < amountOfItems; i++)
            {
                double weight = Math.Round(random.NextDouble() * (max - min) + min, 2);
                cargoList.Add(new("Cargo", weight, (Teu)1));
            }
            foreach (Cargo cargo in cargoList)
            {
                harbor.AddCargo(cargo);
            }

            harbor.AddPort(new("Medium Port", ShipSize.Medium));
            harbor.AddPort(new("Large Port", ShipSize.Large));

            harbor.AddShip(new("Apples", ShipSize.Small, 4, start, "Busan"));
            harbor.AddShip(new("Butter", ShipSize.Medium, 10, start.AddDays(2), "Antwerp"));
            harbor.AddShip(new("Charlie", ShipSize.Large, 24, start, "Shanghai", 2, RecurringSailing.Weekly));
            harbor.DockShips();

            // --------------------
            // Starter Simulasjonen
            // --------------------
            StartSimulation(start, harbor);

            // -----------------
            // Uthenting av data
            // -----------------

            WriteOutData(harbor);


        }

        static void WriteOutData(Harbor harbor)
        {

            Console.WriteLine();
            Console.WriteLine("Warehouses:");
            foreach (Warehouse warehouse in harbor.GetWarehouses())
            {
                Console.WriteLine($"{warehouse}");
                foreach (Cargo cargo in warehouse.GetInventory())
                {
                    Console.WriteLine($"\t{cargo}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Ports:");
            foreach (Port port in harbor.GetPorts())
            {
                Console.WriteLine($"{port}");
                if (port.OccupyingShip != null)
                {
                    foreach (Cargo cargo in port.OccupyingShip.GetCargohold())
                    {
                        Console.WriteLine($"\t{cargo}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Ships in queue:");
            Console.WriteLine(string.Join("\n", harbor.GetWaitingQueue()));

            Console.WriteLine();
            Console.WriteLine("Ships sailing:");
            Console.WriteLine(string.Join("\n", harbor.GetSailingShips()));


            // Uthenting av historikk for spesifikk et skip
            Ship? shipToGetHistoryOn = harbor.GetShipByName("Butter");
            if (shipToGetHistoryOn != null)
            {
                Console.WriteLine();
                Console.WriteLine(shipToGetHistoryOn);
                foreach (LogEntry logEntry in shipToGetHistoryOn.GetLog())
                    // Filtrerer bort alt med cargo fordi listen blir for lang
                    if (!logEntry.ToString().Contains("cargohold"))
                        Console.WriteLine("\t" + logEntry);
            }
            // Denne listen blir laaaang
            //Console.WriteLine();
            //Cargo? cargoToGetHistoryOn = harbor.GetCargoById(2);
            //if (cargoToGetHistoryOn != null)
            //	foreach (LogEntry logEntry in cargoToGetHistoryOn.Log)
            //		Console.WriteLine("logEntry);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}