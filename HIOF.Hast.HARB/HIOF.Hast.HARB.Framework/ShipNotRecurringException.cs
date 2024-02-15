using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
