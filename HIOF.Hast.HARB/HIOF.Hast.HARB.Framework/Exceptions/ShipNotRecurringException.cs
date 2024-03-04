namespace HIOF.Hast.HARB.Framework
{
    [Serializable]
    internal class ShipNotRecurringException : Exception
    {
        public string Ship { get; }

        public ShipNotRecurringException() { }

        public ShipNotRecurringException(string message)
            : base(message) { }


        public ShipNotRecurringException(string message, string name)
            : this(message)
        {
            Ship = name;
        }
    }
}
