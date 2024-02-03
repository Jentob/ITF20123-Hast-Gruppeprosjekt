using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
	/// <summary>
	/// A enum class for storing ship sizes.
	/// </summary>
	public enum ShipSize
	{
		Small,
		Medium,
		Large,
	}
	/// <summary>
	/// A structure class for logging ships.
	/// </summary>
	public struct LogEntry
	{
		public DateTime date;
		public string message;
	}

	/// <summary>
	/// A ship constructor for registering ship objects.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="size"></param>
	/// <param name="maxCarryWeightInKG"></param>
	public class Ship(string name, ShipSize size, int maxCarryWeightInKG)
	{
		private static int count = 0;
		public int Id { get; } = count++;
		public string Name { get; set; } = name;
		public ShipSize Size { get; set; } = size;
		public List<LogEntry> History { get; } = [];
		public List<ICargo> Cargohold { get; } = [];
		public int MaxCargoWeightInKG { get; set; } = maxCarryWeightInKG;
        public List<SailingSchedule> SailingSchedules { get; } = new List<SailingSchedule>();



        public void AddToSailingSchedule(DateTime departureTime, TimeSpan interval)
        {
            SailingSchedules.Add(new SailingSchedule(departureTime, interval));
        }


        public override string? ToString()
		{
			return base.ToString();
		}

		/// <summary>
		/// Add an object that implements the ICargo-interface to the CargoHold
		/// </summary>
		/// <returns>
		/// A bool that specifies whether the object was successfully added or not
		/// </returns>
		/// <param name="cargo">
		/// The cargo to be loaded onboard the ship
		/// </param>
		public bool AddCargo(ICargo cargo)
		{
			int weight = cargo.WeightInKG;
			foreach (var item in Cargohold)
			{
				weight += item.WeightInKG;
			}
			if (weight > MaxCargoWeightInKG)
			{
				History.Add(new LogEntry()
				{
					date = DateTime.Now,
					message = $"{cargo.Name} ({cargo.Id}) failed to add to cargohold. Ship is full"
				});
				return false;
			}
			cargo.RecordHistory(new LogEntry() { date = DateTime.Now, message = $"Cargo moved onboard the ship {this.Name} ({this.Id})" });
			Cargohold.Add(cargo);

			History.Add(new LogEntry()
			{
				date = DateTime.Now,
				message = $"{cargo.Name} ({cargo.Id}) added to cargohold"
			});
			
			Console.WriteLine($"Container '{cargo.Name}' er lastet på skipet {Name}.");

			return true;
		}

		/// <summary>
		/// Remove cargo from the ship object.
		/// </summary>
		/// <param name="cargo"></param>
		/// <returns>Returns the cargo interface. </returns>
		public ICargo? RemoveCargo(ICargo cargo)
		{
			if (!Cargohold.Contains(cargo)) return null;
			Cargohold.Remove(cargo);
			History.Add(new LogEntry()
			{
				date = DateTime.Now,
				message = $"{cargo.Name} ({cargo.Id}) removed from cargohold"
			});
			cargo.RecordHistory(new LogEntry() { date = DateTime.Now, message = $"Cargo moved off the ship {this.Name} ({this.Id})" });
			Console.WriteLine($"Container '{cargo.Name}' er lastet av skipet {Name}.");

			return cargo;
		}


		/// <summary>
		/// A method for printing Cargohold
		/// </summary>
		public void PrintCargohold()
		{
			if (Cargohold.Count != 0)
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


		/// <summary>
		/// A method for printing cargo history
		/// </summary>
		public void PrintHistory()
		{
			foreach (LogEntry entry in History) { Console.WriteLine($"{entry.date} - {entry.message}"); }
		}

	}
}
