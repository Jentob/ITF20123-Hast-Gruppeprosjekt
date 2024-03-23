using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace HIOF.Hast.HARB.Framework
{
    public class CargoTruck
    {
        public int Id { get; }
        public bool IsLoaded { get; private set; }
        public Cargo? LoadedCargo { get; private set; }

        public CargoTruck(int id)
        {
            Id = id;
            IsLoaded = false;
        }

        public void LoadCargo(Cargo cargo)
        {
            if (!IsLoaded)
            {
                LoadedCargo = cargo;
                IsLoaded = true;
                Console.WriteLine($"Truck {Id} loaded with cargo {cargo.Id}");
            }
            else
            {
                Console.WriteLine($"Truck {Id} is already loaded with cargo.");
            }
        }

        public void UnloadCargo()
        {
            if (IsLoaded)
            {
                Console.WriteLine($"Truck {Id} unloaded cargo.");
                LoadedCargo = null;
                IsLoaded = false;
            }
            else
            {
                Console.WriteLine($"Truck {Id} is not loaded with cargo.");
            }
        }
    }
}

