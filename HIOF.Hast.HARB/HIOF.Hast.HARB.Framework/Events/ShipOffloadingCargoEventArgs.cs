namespace HIOF.Hast.HARB.Framework.Events
{
    public class ShipOffloadingCargoEventArgs : EventArgs
    {
        public ShipOffloadingCargoEventArgs(Cargo cargoOffloaded)
        {
            CargoOffloaded = cargoOffloaded;
        }

        public Cargo CargoOffloaded { get; private set; }
    }
}