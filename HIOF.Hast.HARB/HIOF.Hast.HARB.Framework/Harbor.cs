using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    public class Harbor
    {
        public List<Ship> Ships { get;} = new List<Ship>();
        public List<Container> Containers { get;} = new List<Container>();

        public void ConfigureHarbor()
        {
            Console.WriteLine("Velkommen til vår havn simulator");

            Console.Write("Skriv in antall skip du ønsker å legge til i simuleringen:");
            int numberOfShips = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfShips; i++)
            {
                Console.Write($"Skriv inn navnet på skip {i + 1}: ");
                string shipName = Console.ReadLine();

                Console.Write($"Velg størrelsen på skipet (Small/Medium/Large) for {shipName}: ");
                ShipSize shipSize = Enum.Parse<ShipSize>(Console.ReadLine(), true);

                Ship ship = new Ship(shipName, shipSize);
                Ships.Add(ship);

                Console.WriteLine($"Skipet {ship} er lagt til i havnen.");
            }
        }
    }
}
