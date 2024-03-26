using HIOF.Hast.HARB.Framework;

namespace HIOF.Hast.HARB.FrameworkImplementation
{
    internal class Delinnlevering3
    {
        static void Main(string[] args)
        {
            DateTime start = new(2024, 01, 01);
            DateTime end = new(2024, 03, 01);

            Harbor harbor = new("Havn");

            Port port;

            port = new("Havneplass 1", ShipSize.Large);
            port.AddCrane();
            port.AddCrane();
            port.AddCrane();
            harbor.AddPort(port);

            port = new("Havneplass 2", ShipSize.Large);
            port.AddCrane();
            port.AddCrane();
            harbor.AddPort(port);

            port = new("Havneplass 3", ShipSize.Large);
            port.AddCrane();
            port.AddCrane();
            harbor.AddPort(port);

            for (int i = 0; i < 24; i++)
                harbor.AddWarehouse(new($"Stor langringskolonne {i + 1}", 18));
            for (int i = 0; i < 7; i++)
                harbor.AddWarehouse(new($"Liten langringskolonne {i + 1}", 15));

            
            Console.WriteLine("Simulation started");
            SimulationDriver.Run(harbor, start, end);
            Console.WriteLine("Simulation ended");
        }
    }
}
