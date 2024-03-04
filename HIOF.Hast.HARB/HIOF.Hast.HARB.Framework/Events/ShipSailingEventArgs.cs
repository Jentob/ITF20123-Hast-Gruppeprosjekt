namespace HIOF.Hast.HARB.Framework
{
    public class ShipSailingEventArgs : EventArgs
    {
        public ShipSailingEventArgs (Ship shipSailing)
        {
            ShipSailing = shipSailing;
        }

        public Ship ShipSailing { get; private set; }
    }
}
