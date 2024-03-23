using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    internal class Crane
    {
        internal int Id { get; private set; }
        internal bool IsAvailable { get; private set; }

        public Crane(int craneId)
        {
            Id = craneId;
            IsAvailable = true; 
        }

        public bool AcquireCrane()
        {
            if (IsAvailable)
            {
                IsAvailable = false; 
                return true; 
            }
            return false; 
        }

        public void ReleaseCrane()
        {
            IsAvailable = true;
        }
    }
}

