namespace HIOF.Hast.HARB.Framework
{
    public class CargoTruck() : ICargoTransport
	{
        private static int idCount = 0;
		public int Id { get; } = idCount++;
		public Cargo? LoadedCargo { get; private set; }

		public bool LoadCargo(Cargo cargo)
        {
            if (LoadedCargo == null)
            {
                LoadedCargo = cargo;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Cargo? UnloadCargo()
        {
            if (LoadedCargo != null)
			{
				Cargo? cargo = LoadedCargo;
				LoadedCargo = null;
				return cargo;
			}
			return null;
		}
    }
}

