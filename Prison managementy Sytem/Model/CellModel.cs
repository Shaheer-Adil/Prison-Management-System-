using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison_managementy_Sytem.Model
{
    internal class CellModel
    {
        public int CellID { get; set; }  // Auto-increment in DB
        public string Block { get; set; }
        public string CellNumber { get; set; }
        public int Capacity { get; set; }
        public int CurrentOccupancy { get; set; }

        // Constructor for creating a new cell (without ID)
        public CellModel(string block, string cellNumber, int capacity, int currentOccupancy)
        {
            Block = block;
            CellNumber = cellNumber;
            Capacity = capacity;
            CurrentOccupancy = currentOccupancy;
        }

        // Constructor with ID (for reading from DB)
        public CellModel(int id, string block, string cellNumber, int capacity, int currentOccupancy)
        {
            CellID = id;
            Block = block;
            CellNumber = cellNumber;
            Capacity = capacity;
            CurrentOccupancy = currentOccupancy;
        }

        public CellModel() { } // Parameterless constructor

        public override string ToString()
        {
            return $"Cell {CellNumber} (Block {Block}), Capacity: {Capacity}, Occupied: {CurrentOccupancy}";
        }
    }
}

