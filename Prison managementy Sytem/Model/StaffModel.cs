using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison_managementy_Sytem.Model
{
    public class StaffModel
    {
        public int StaffID { get; set; } // Auto-increment in DB
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string Shift { get; set; }

        // Constructor for new staff (without ID)
        public StaffModel(string fullName, string role, string phone, string shift)
        {
            FullName = fullName;
            Role = role;
            Phone = phone;
            Shift = shift;
        }

        // Constructor with ID (for reading from DB)
        public StaffModel(int id, string fullName, string role, string phone, string shift)
        {
            StaffID = id;
            FullName = fullName;
            Role = role;
            Phone = phone;
            Shift = shift;
        }

        public StaffModel() { } // Parameterless constructor

        public override string ToString()
        {
            return $"{FullName} ({Role}), Phone: {Phone}, Shift: {Shift}";
        }
    }
}

