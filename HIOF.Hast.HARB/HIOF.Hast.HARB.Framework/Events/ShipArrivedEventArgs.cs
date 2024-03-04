namespace HIOF.Hast.HARB.Framework
{
    public class ShipArrivedEventArgs : EventArgs
    {
        public ShipArrivedEventArgs (Ship shipArrived)
        {
            ShipArrived = shipArrived;
        }

        public Ship ShipArrived { get; private set; }
    }
}