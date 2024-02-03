using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace HIOF.Hast.HARB.Framework
{
    public class Harbour
    {
        public Queue<Ship> WaitingQueue { get; } = new Queue<Ship>();
        public List<Port> Port { get; } = new List<Port> { };

        /// <summary>
        /// Metode for å konfigurere havnen. 
        /// </summary>
        public void ConfigureHarbour()
        {
            Console.WriteLine("Velkommen til vår havn simulator");

            // Håndter antall skip
            int numberOfShips = GetNumberOfShips();

            // Opprett skip basert på brukerinput
            for (int i = 0; i < numberOfShips; i++)
            {
                Ship ship = CreateShip(i + 1);
                WaitingQueue.Enqueue(ship);

                Console.WriteLine($"Skipet {ship.Name} er lagt til i havnen.");
            }
        }

        /// <summary>
        /// Metode for å sette antall skip som skal legges til havn 
        /// </summary>
        private int GetNumberOfShips()
        {
            Console.Write("Skriv inn antall skip du ønsker å legge til for simulering: ");
            int numberOfShips;

            while (!int.TryParse(Console.ReadLine(), out numberOfShips) || numberOfShips <= 0)
            {
                Console.Write("Ugyldig tall, skriv inn et positivt heltall: ");
            }

            return numberOfShips;
        }

        /// <summary>
        /// Metode for å opprette skip til havnen: navn, størrelse og om det ønskes gjentagende seilinger
        /// </summary>
        private Ship CreateShip(int index)
        {
            Console.Write($"Skriv inn navnet på skip {index}: ");
            string shipName = Console.ReadLine();

            Console.Write($"Velg størrelsen på skipet (Small/Medium/Large) for {shipName}: ");
            ShipSize shipSize = Enum.Parse<ShipSize>(Console.ReadLine(), true);

            Console.Write($"Ønsker du gjentagende seilinger for {shipName}? (Ja/Nei): ");
            bool hasRecurringSailings = Console.ReadLine().Trim().Equals("Ja", StringComparison.OrdinalIgnoreCase);

            Ship ship = new Ship(shipName, shipSize);

            // Legg til logikk for gjentagende seilinger
            if (hasRecurringSailings)
            {

            }

            return ship;
        }
    }
}
