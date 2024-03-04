namespace HIOF.Hast.HARB.Framework.Events
{
    public class ShipLoadingCargoEventArgs : EventArgs
    {
        public ShipLoadingCargoEventArgs(Cargo cargoLoaded)
        {
            CargoLoaded = cargoLoaded;
        }

        public Cargo CargoLoaded { get; private set; }
    }
}