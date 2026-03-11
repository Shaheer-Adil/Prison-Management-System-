using System;

namespace Prison_managementy_Sytem.Model
{
    public class Transfer
    {
        public int TransferID { get; set; } // Auto-increment in DB
        public int PrisonerID { get; set; } // Foreign key
        public string FromPrison { get; set; }
        public string ToPrison { get; set; }
        public DateTime? TransferDate { get; set; }
        public string ApprovedBy { get; set; }

        // Constructor for new transfer
        public Transfer(int prisonerID, string fromPrison, string toPrison, DateTime transferDate, string approvedBy)
        {
            PrisonerID = prisonerID;
            FromPrison = fromPrison;
            ToPrison = toPrison;
            TransferDate = transferDate;
            ApprovedBy = approvedBy;

        }
        public Transfer( int id ,int prisonerID, string fromPrison, string toPrison, DateTime transferDate, string approvedBy)
        {
            TransferID = id;
            PrisonerID = prisonerID;
            FromPrison = fromPrison;
            ToPrison = toPrison;
            TransferDate = transferDate;
            ApprovedBy = approvedBy;


        }



        public Transfer() { }

        public override string ToString()
        {
            return $"{TransferID}: {FromPrison} -> {ToPrison} on {TransferDate:d}, Approved by {ApprovedBy}";
        }
    }
}
