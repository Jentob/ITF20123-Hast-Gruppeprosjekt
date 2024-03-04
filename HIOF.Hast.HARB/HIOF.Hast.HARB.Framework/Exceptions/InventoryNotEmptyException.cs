namespace HIOF.Hast.HARB.Framework
{
    [Serializable]
    internal class InventoryNotEmptyException : Exception
    {
        public InventoryNotEmptyException()
        {
        }

        public InventoryNotEmptyException(string message)
            : base(message)
        {
        }
    }
}
