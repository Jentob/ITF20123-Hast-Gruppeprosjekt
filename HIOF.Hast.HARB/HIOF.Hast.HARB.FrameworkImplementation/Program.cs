using HIOF.Hast.HARB.Framework;

namespace HIOF.Hast.HARB.FrameworkImplementation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Harbour harbour = new Harbour();

            // Creating Harbour
            harbour.ConfigureHarbour();

            harbour.GetNumberOfShips();

            Console.WriteLine("Simulasjon av havn startet. . .");

            /*ISimulationDriver driver = new DriverImplementation();
            driver.Run();*/

            Console.WriteLine("Simulasjon er ferdig. Skriv inn ID på skip for å se historikk:");

            // implementer 


        }
    }
}