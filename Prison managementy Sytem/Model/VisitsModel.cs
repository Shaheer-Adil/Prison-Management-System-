using System;

namespace Prison_managementy_Sytem.Model
{
    public class Visit
    {
        public int VisitID { get; set; } // Auto-increment in DB
        public int PrisonerID { get; set; } // Foreign key
        public int ?VisitorID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string Purpose { get; set; }
        public int? ApprovedByStaffID { get; set; }

        // Constructor for new visit
        public Visit(int prisonerID, int visitorID, DateTime visitDate, string purpose, int approvedByStaffID)
        {
            PrisonerID = prisonerID;
            VisitorID = visitorID;
            VisitDate = visitDate;
            Purpose = purpose;
            ApprovedByStaffID = approvedByStaffID;
        }
        public Visit(int id,int prisonerID, int visitorID, DateTime visitDate, string purpose, int approvedByStaffID)
        {
            VisitID = id;
            PrisonerID = prisonerID;
            VisitorID = visitorID;
            VisitDate = visitDate;
            Purpose = purpose;
            ApprovedByStaffID = approvedByStaffID;
        }

        public Visit() { }

        public override string ToString()
        {
            return $"{VisitID}: Visitor {VisitorID} on {VisitDate:d}, Purpose: {Purpose}";
        }
    }
}
