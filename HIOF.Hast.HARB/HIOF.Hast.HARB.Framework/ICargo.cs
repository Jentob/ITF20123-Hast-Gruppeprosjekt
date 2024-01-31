using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    public interface ICargo
    {
        public int Id { get; }
        public string Name { get; }
        public int WeightInKG { get; }
    }
}
