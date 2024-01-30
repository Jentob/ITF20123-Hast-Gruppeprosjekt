using HIOF.Hast.HARB.Framework;

namespace HIOF.Hast.HARB.FrameworkImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISimulationDriver driver = new DriverImplementation();
            driver.Run();
        }
    }
}