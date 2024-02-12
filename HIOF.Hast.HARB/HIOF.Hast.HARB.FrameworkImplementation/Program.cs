using System.Net.Sockets;
using HIOF.Hast.HARB.Framework;

namespace HIOF.Hast.HARB.FrameworkImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Harbor harbor = new("Harbor 1");

            DateTime start = new(2024, 01, 01);
            DateTime end = new(2024, 04, 01);

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
				cargoList.Add(new("Cargo", weight));
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

			// -------------------
			// Starter Simulasjonen
			// -------------------
			Console.WriteLine("Simulation started");
			Driver.Run(harbor, start, end);
			Console.WriteLine("Simulation ended");

			// -----------------
			// Uthenting av data
			// -----------------
			Console.WriteLine();
			Console.WriteLine("Warehouses:");
			foreach (Warehouse warehouse in harbor.Warehouses)
			{
				Console.WriteLine($"{warehouse}");
				foreach (Cargo cargo in warehouse.Inventory)
				{
					Console.WriteLine($"\t{cargo}");
				}
			}

			Console.WriteLine();
			Console.WriteLine("Ports:");
			foreach (Port port in harbor.Ports)
			{
				Console.WriteLine($"{port}");
				if (port.OccupyingShip != null)
				{
					foreach (Cargo cargo in port.OccupyingShip.Cargohold)
					{
						Console.WriteLine($"\t{cargo}");
					}
				}
			}

			Console.WriteLine();
			Console.WriteLine("Ships in queue:");
			Console.WriteLine(string.Join("\n", harbor.WaitingQueue));

			Console.WriteLine();
			Console.WriteLine("Ships sailing:");
			Console.WriteLine(string.Join("\n", harbor.SailingShips));


			// TODO: Lage en metode som retunerner en list med alle skip
			// TODO: Lage en metode som returnerer en list med alle cargo-objeckter fra alle skip og lagere
		}
	}
}