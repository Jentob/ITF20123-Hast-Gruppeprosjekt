using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
	/// <summary>
	/// A abstract class for registering Cargo objects, that implements the ICargo interface.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="weight"></param>
	/// <param name="description"></param>
	internal abstract class Cargo(string name, int weight, string description) : ICargo
    {
		private static int count;
		public int Id { get; } = count++;
		public string Name { get; set; } = name;
		public int WeightInKG { get; set; } = weight;
		public string Description { get; set; } = description;
		public List<LogEntry> History { get; } = [];

		public void RecordHistory(LogEntry entry)
		{
			History.Add(entry);
		}

	}
}
