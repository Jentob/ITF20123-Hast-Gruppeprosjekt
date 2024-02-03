using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    public interface ISimulationDriver
    {
        public void Run(Harbour harbor, int time);
    }
}
