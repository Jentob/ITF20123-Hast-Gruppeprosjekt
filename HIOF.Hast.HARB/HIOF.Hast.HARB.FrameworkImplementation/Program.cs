using HIOF.Hast.HARB.Framework;

namespace HIOF.Hast.HARB.FrameworkImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Harbor harbor = new();

            DateTime start = DateTime.Parse("2024-01-01");
            DateTime end = DateTime.Parse("2024-04-01");

            // TODO: Her setter vi opp havnen

            Driver.Run(harbor, start, end);

            // TODO: Her kan vi hente ut data
            
        }
    }
}