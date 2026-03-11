using System;

namespace Prison_managementy_Sytem.Model
{
    public class Crime
    {
        public int CrimeID { get; set; } // Auto-increment in DB
        public int PrisonerID { get; set; } // Foreign key
        public string CrimeType { get; set; }
        public string Description { get; set; }
        public DateTime? CrimeDate { get; set; }
        public int SentenceYears { get; set; }

        // Constructor for creating a new crime (without CrimeID)
        public Crime(int prisonerID, string crimeType, string description, DateTime crimeDate, int sentenceYears)
        {
            PrisonerID = prisonerID;
            CrimeType = crimeType;
            Description = description;
            CrimeDate = crimeDate;
            SentenceYears = sentenceYears;
        }
        public Crime(int id, int prisonerID, string crimeType, string description, DateTime crimeDate, int sentenceYears)
        {
            CrimeID = id;
            PrisonerID = prisonerID;
            CrimeType = crimeType;
            Description = description;
            CrimeDate = crimeDate;
            SentenceYears = sentenceYears;
        }

        public Crime() { } // For reading from DB

        public override string ToString()
        {
            return $"{CrimeID}: {CrimeType} on {CrimeDate:d}, Sentence: {SentenceYears} years";
        }
    }
}
