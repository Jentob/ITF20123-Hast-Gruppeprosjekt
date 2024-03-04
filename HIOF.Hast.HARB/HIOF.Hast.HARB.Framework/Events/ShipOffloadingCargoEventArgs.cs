namespace HIOF.Hast.HARB.Framework
{
    public class ShipOffloadingCargoEventArgs(Cargo cargoOffloaded) : EventArgs
    {
        public Cargo CargoOffloaded { get; private set; } = cargoOffloaded;
    }
}