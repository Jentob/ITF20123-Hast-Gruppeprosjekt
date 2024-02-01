using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
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
