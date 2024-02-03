using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIOF.Hast.HARB.Framework
{
    public interface ISimulationDriver
    {
        /// <param name="time"> Ventetiden på venteplass.</param>

        public int Run(int time);

        /// <summary>
        /// Stop simulation
        /// </summary>
        public void Step();
    }
}
