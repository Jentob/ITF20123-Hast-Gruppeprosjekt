namespace HIOF.Hast.HARB.Framework
{
    [Serializable]
    internal class ShipNoRecurringException : Exception
    {
        public ShipNoRecurringException()
        {
        }

        public ShipNoRecurringException(string message)
            : base(message)
        {
        }
    }
}
