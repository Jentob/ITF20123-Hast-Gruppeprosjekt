namespace HIOF.Hast.HARB.Framework
{
    public class ShipSailingEventArgs(Ship shipSailing) : EventArgs
    {
        public Ship ShipSailing { get; private set; } = shipSailing;
    }
}
