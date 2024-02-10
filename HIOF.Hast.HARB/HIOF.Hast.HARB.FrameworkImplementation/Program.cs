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

            harbor.AddWarehouse(new("Warehouse 1", 10));
			harbor.AddWarehouse(new("Warehouse 2", 5));

			harbor.AddPort(new("Medium Port 1", ShipSize.Medium));
			harbor.AddPort(new("Large Port 1", ShipSize.Large));

			harbor.AddShip(new("Apple", ShipSize.Small, 2));
			harbor.AddShip(new("Butter", ShipSize.Medium, 8));
			harbor.AddShip(new("Charlie", ShipSize.Large, 20));
            harbor.DockShips();


			Driver.Run(harbor, start, end);


            Console.WriteLine(String.Join("\n", harbor.Ports));
			Console.WriteLine();
			Console.WriteLine("Ships in queue:");
			Console.WriteLine(String.Join("\n", harbor.WaitingQueue));


		}
	}
}