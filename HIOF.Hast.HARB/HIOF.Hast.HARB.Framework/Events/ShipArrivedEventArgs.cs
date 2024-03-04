namespace HIOF.Hast.HARB.Framework
{
    public class ShipArrivedEventArgs(Ship shipArrived) : EventArgs
    {
        public Ship ShipArrived { get; private set; } = shipArrived;
    }
}