
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison_managementy_Sytem.Model
{
    public class VisitorsModel

    {
        public int VisitorID { get; set; } // Auto-increment in DB
        public string VisitorName { get; set; }
        public string CNIC { get; set; }
        public string Phone { get; set; }

        // Optional: List of visits by this visitor
        public List<Visit> Visits { get; set; }

        // Constructor for new visitor (without ID)
        public VisitorsModel(string visitorName, string cnic, string phone)
        {
            VisitorName = visitorName;
            CNIC = cnic;
            Phone = phone;
            Visits = new List<Visit>();
        }

        // Constructor with ID (for reading from DB)
        public VisitorsModel(int id, string visitorName, string cnic, string phone)
        {
            VisitorID = id;
            VisitorName = visitorName;
            CNIC = cnic;
            Phone = phone;
            Visits = new List<Visit>();
        }

        public VisitorsModel()
        {
            Visits = new List<Visit>();
        } // Parameterless constructor

        public override string ToString()
        {
            return $"{VisitorName}, CNIC: {CNIC}, Phone: {Phone}";
        }
    }
}
