﻿namespace HIOF.Hast.HARB.Framework
{
	/// <summary>Represents cargo. Implements the ICargo interface.</summary>
	/// <param name="name">Name of cargo</param>
	/// <param name="weight">Weight of cargo in tons</param>
	public class Cargo(string name, double weightInTons)
    {
		/// <summary>Used to auto increment <see cref="Id"/>.</summary>
		private static int idCount = 0;
		/// <summary>Represents a unique identifier for each cargo-object.</summary>
		public int Id { get; } = idCount++;
		/// <summary>Name of cargo.</summary>
		public string Name { get; set; } = name;
		/// <summary>Weight of cargo in tons.</summary>
		public double WeightInTons { get; } = weightInTons;
		/// <summary>Keeps track of events related to the cargo-object.</summary>
		public List<LogEntry> Log { get; } = [];
		/// <summary>Adds a record to <see cref="Log"/></summary>
        internal void RecordHistory(LogEntry entry) => Log.Add(entry);

        public override string ToString() => $"{GetType().Name} - {Name}({Id}) - {WeightInTons} metric tons";
    }
}
