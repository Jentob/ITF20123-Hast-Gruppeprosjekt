namespace HIOF.Hast.HARB.Framework
{
    public class Port
    {
        public string Name { get; }

        
        public bool IsOccupied { get; private set; }

        public Port(string name)
        {
            Name = name;
            IsOccupied = false;

        }

        public void OccupyPort()
        {
            IsOccupied = true;
        }

        public void LeavePort()
        {
            IsOccupied = false;
        }

    }
}