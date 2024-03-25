namespace HIOF.Hast.HARB.Framework
{
    public class Crane()
    {
        private static int idCount = 0;
        public int Id { get; } = idCount++;
        private bool OccupyCrane { get; set; } = true;
        private bool CraneAvailable { get; set; } = false;
    }
}

