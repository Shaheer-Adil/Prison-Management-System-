using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.Xml;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Prison_managementy_Sytem.Model
{
    public class PrisonerModel
    {
        public int PrisonerID { get; set; } // Auto-increment in DB
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Status { get; set; }
        public int? CellID { get; set; }

        public List<Crime> Crimes { get; set; }
        public List<Transfer> Transfers { get; set; }
        public List<Visit> Visits { get; set; }




        public PrisonerModel()
        {

        }
        // Constructor for creating a new prisoner (without ID)
        public PrisonerModel(string firstName, string lastName, string gender, DateTime dob,
                             DateTime entryDate, DateTime releaseDate, string status, int cellID)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DOB = dob;
            EntryDate = entryDate;
            ReleaseDate = releaseDate;
            Status = status;
            CellID = cellID;

            Crimes = new List<Crime>();
            Transfers = new List<Transfer>();
            Visits = new List<Visit>();
        }
        public PrisonerModel( int id,string firstName, string lastName, string gender, DateTime dob,
                             DateTime entryDate, DateTime releaseDate, string status, int cellID)
        {
            PrisonerID = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            DOB = dob;
            EntryDate = entryDate;
            ReleaseDate = releaseDate;
            Status = status;
            CellID = cellID;

            Crimes = new List<Crime>();
            Transfers = new List<Transfer>();
            Visits = new List<Visit>();
        }

        // Parameterless constructor (needed for reading from DB)


        public override string ToString()
        {
            return $"{PrisonerID}: {FirstName} {LastName}, Gender: {Gender}, DOB: {DOB:d}, Status: {Status}";
        }
    }
}
