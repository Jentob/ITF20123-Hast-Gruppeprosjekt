using HIOF.Hast.HARB.Framework;

namespace HIOF.Hast.HARB.FrameworkImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Harbor harbor = new("Harbor 1");

            DateTime start = DateTime.Parse("2024-01-01");
            DateTime end = DateTime.Parse("2024-02-01");

            harbor.AddWarehouse(new("Large warehouse", 10));
			harbor.AddWarehouse(new("Small warehouse", 5));

			harbor.AddPort(new("Medium Port", ShipSize.Medium));
			harbor.AddPort(new("Large Port", ShipSize.Large));

			harbor.AddShip(new("Apples", ShipSize.Small, 2));
			harbor.AddShip(new("Butter", ShipSize.Medium, 8));
			harbor.AddShip(new("Charlie", ShipSize.Large, 20));
            harbor.DockShips();

			// TODO: Cargo??? Kanskje legge cargo til i lagerne siden det er enklest


			Console.WriteLine("Simulation started");
			Driver.Run(harbor, start, end);
			Console.WriteLine("Simulation ended");


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
            Console.WriteLine(string.Join("\n", harbor.Ports));
			Console.WriteLine();
			Console.WriteLine("Ships in queue:");
			Console.WriteLine(string.Join("\n", harbor.WaitingQueue));

			// TODO: Hvis det er et spesifikt skip eller last man ønsker informasjon på, hvordan skal man hente det ut?
		}
	}
}