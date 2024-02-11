namespace HIOF.Hast.HARB.Framework
{
	/// <summary>Represents cargo. Implements the ICargo interface.</summary>
	/// <param name="name">Name of cargo</param>
	/// <param name="weight">Weight in tons</param>
	public class Cargo(string name, double weight)
    {
		private static int idCount = 0;
		public int Id { get; } = idCount++;
		public string Name { get; set; } = name;
		public double WeightInTons { get; } = weight;
		public List<LogEntry> Log { get; } = [];

        internal void RecordHistory(LogEntry entry) => Log.Add(entry);

        public override string ToString() => $"{GetType().Name} - {Name}({Id}) - {WeightInTons} metric tons";
    }
}
