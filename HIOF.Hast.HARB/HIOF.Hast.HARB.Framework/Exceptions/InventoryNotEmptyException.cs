using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    [Serializable]
    internal class InventoryNotEmptyException : Exception
    {
        public int Amount { get; }

        public InventoryNotEmptyException() { }

        public InventoryNotEmptyException(string message)
            : base(message) { }


        public InventoryNotEmptyException(string message, int amount)
            : this(message)
        {
            Amount = amount;
        }
    }
}
