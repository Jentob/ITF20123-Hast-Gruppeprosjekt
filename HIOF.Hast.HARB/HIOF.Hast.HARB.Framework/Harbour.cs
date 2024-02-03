﻿using System;
using System.Collections.Generic;

namespace HIOF.Hast.HARB.Framework
{
    /// <summary>
    /// Class representing a harbour simulator.
    /// </summary>
    public class Harbour
    {
        /// <summary>
        /// Gets the queue of ships waiting to enter the harbour.
        /// </summary>
        public Queue<Ship> WaitingQueue { get; } = new Queue<Ship>();
        /// <summary>
        /// Gets the list of ports in the harbour.
        /// </summary>
        public List<Port> Port { get; } = new List<Port> { };
      

        /// <summary>
        /// Method to configure the harbour.
        /// </summary>
        /// 
        public void ConfigureHarbour()
        {
            Console.WriteLine("Welcome to our harbor simulator");

            // Handle the number of ships
            int numberOfShips = GetNumberOfShips();

            // Create ships based on user input
            for (int i = 0; i < numberOfShips; i++)
            {
                Ship ship = CreateShip(i + 1);
                WaitingQueue.Enqueue(ship);

                Console.WriteLine($"The ship {ship.Name} has been added to the harbour.");
            }

        }

        /// <summary>
        /// Method to set the number of ships to be added to the harbour.
        /// </summary>
        private int GetNumberOfShips()
        {
            Console.Write("Enter the number of ships you want to add for simulation: ");
            int numberOfShips;

            while (!int.TryParse(Console.ReadLine(), out numberOfShips) || numberOfShips <= 0)
            {
                Console.Write("Invalid number, please enter a positive integer: ");
            }

            return numberOfShips;
        }

        /// <summary>
        /// Method to create a ship for the harbour: name, size, and whether recurring sailings are desired.
        /// </summary>
        private Ship CreateShip(int index)
        {
            Console.Write($"Enter the name of ship {index}: ");
            string shipName = Console.ReadLine();

            Console.Write($"Choose the size of the ship (Small/Medium/Large) for {shipName}: ");
            ShipSize shipSize = Enum.Parse<ShipSize>(Console.ReadLine(), true);

            Console.Write($"Do you want recurring sailings for {shipName}? (Yes/No): ");
            bool hasRecurringSailings = Console.ReadLine().Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);

            Ship ship = new Ship(shipName, shipSize);

            // TODO: Add logic for recurring sailings
            if (hasRecurringSailings)
            {
              
            }
            return ship;
        }
    }
}
