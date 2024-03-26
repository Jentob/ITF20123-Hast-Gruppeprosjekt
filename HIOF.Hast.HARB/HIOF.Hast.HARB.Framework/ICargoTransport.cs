namespace HIOF.Hast.HARB.Framework
{
	public interface ICargoTransport
	{
		public Cargo? LoadedCargo { get; }

		public bool LoadCargo(Cargo cargo);

		public Cargo? UnloadCargo();
	}
}
