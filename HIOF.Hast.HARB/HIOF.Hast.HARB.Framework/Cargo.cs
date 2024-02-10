namespace HIOF.Hast.HARB.Framework
{
	/// <summary>Represents cargo. Implements the ICargo interface.</summary>
	/// <param name="name">Name of cargo</param>
	/// <param name="weight">Weight in kilos</param>
	public class Cargo(string name, int weight) : ICargo
    {
		private static int idCount = 0;
		public int Id { get; } = idCount++;
		public string Name { get; set; } = name;
		public double WeightInKG { get; } = weight;
		public List<LogEntry> Log { get; } = [];

        public void RecordHistory(LogEntry entry) => Log.Add(entry);

        public override string ToString() => $"Cargo - {Name}({Id}) - {WeightInKG}";
    }
}
