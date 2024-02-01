using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
	public enum ShipSize
	{
		Small,
		Medium,
		Large,
	}
	public struct LogEntry
	{
		public DateTime date;
		public string message;
	}

	public class Ship(string name, ShipSize size)
	{
		private static int count = 0;
		public int Id { get; } = count++;
		public string Name { get; set; } = name;
		public ShipSize Size { get; set; } = size;
        public List<LogEntry> History { get; } = new List<LogEntry>();
        public List<ICargo> Cargohold { get; } = new List<ICargo>();

        public override string? ToString()
        {
            return base.ToString();
        }


        public void AddCargo(ICargo cargo)
        {
            Cargohold.Add(cargo);
            Console.WriteLine($"Container '{cargo.Name}' er lastet på skipet {Name}.");
            // legg til i loggen
        }


        public void PrintCargohold()
        {
            if (Cargohold.Any())
            {
                Console.WriteLine($"Liste over containere for skipet {Name}:");
                foreach (ICargo cargo in Cargohold)
                {
                    Console.WriteLine($"{cargo.Name} - Vekt: {cargo.WeightInKG} kg");
                }
            }
            else
            {
                Console.WriteLine($"Ingen containerer på skipet {Name}.");
            }
        }


		public void PrintHistory()
		{
			foreach (LogEntry entry in History) { Console.WriteLine(entry.message); }
		}

	}
}
