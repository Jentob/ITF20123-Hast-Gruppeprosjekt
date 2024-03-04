namespace HIOF.Hast.HARB.Framework
{
    public class ShipLoadingCargoEventArgs(Cargo cargoLoaded) : EventArgs
    {
        public Cargo CargoLoaded { get; private set; } = cargoLoaded;
    }
}