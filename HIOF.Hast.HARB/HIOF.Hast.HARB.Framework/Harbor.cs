using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace HIOF.Hast.HARB.Framework
{
    public class Harbor
    {
        public List<Ship> Ships { get;} = new List<Ship>();

        /// <summary>
        /// Metode for å konfigurere havnen. 
        /// </summary>
        public void ConfigureHarbor()
        {
            Console.WriteLine("Velkommen til vår havn simulator");

            Console.Write("Skriv in antall skip du ønsker å legge til i simuleringen:");
            int numberOfShips;

           
            // Valider brukeren sin input
            while (!int.TryParse(Console.ReadLine(), out numberOfShips) || numberOfShips <= 0)
            {
                Console.Write("Ugyldig tall, skriv inn et positivt heltall: ");
            }

            // Navn og størrelse på skipene
            for (int i = 0; i < numberOfShips; i++)
            {
                Console.Write($"Skriv inn navnet på skip {i + 1}: ");
                string shipName = Console.ReadLine();

                Console.Write($"Velg størrelsen på skipet (Small/Medium/Large) for {shipName}: ");
                ShipSize shipSize = Enum.Parse<ShipSize>(Console.ReadLine(), true);

                Console.Write($"Ønsker du gjentagende seilinger for {shipName}? ");
                // Mulighet for å sette opp gjenntagende seilinger

                Ship ship = new Ship(shipName, shipSize);
                Ships.Add(ship);

                // Legge til cargo


                Console.WriteLine($"Skipet {ship} er lagt til i havnen.");
            }
        }
    }
}
