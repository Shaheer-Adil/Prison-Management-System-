using Prison_managementy_Sytem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Prison_managementy_Sytem.Repo
{ 
    internal class PrisonerRepo
    {
        private readonly string connectionString = Util.connection();

        public PrisonerRepo()
        {
            connectionString = Util.connection();
        }

        // INSERT Prisoner + Crimes + Transfers + Visits
        public void AddPrisonerWithDetails(PrisonerModel prisoner)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {


                        string prisonerQuery = @"
                            INSERT INTO Prisoners 
                                (FirstName, LastName, Gender, DOB, EntryDate, ReleaseDate, Status, CellID)
                            VALUES (@fn,@ln,@g,@dob,@entry,@rel,@st,@cid);
                            SELECT SCOPE_IDENTITY();";

                        int prisonerId;
                        using (SqlCommand cmd = new SqlCommand(prisonerQuery, con, tx))
                        {
                            cmd.Parameters.Add("@fn", SqlDbType.NVarChar).Value = (object)prisoner.FirstName ?? DBNull.Value;
                            cmd.Parameters.Add("@ln", SqlDbType.NVarChar).Value = (object)prisoner.LastName ?? DBNull.Value;
                            cmd.Parameters.Add("@g", SqlDbType.NVarChar).Value = (object)prisoner.Gender ?? DBNull.Value;
                            cmd.Parameters.Add("@dob", SqlDbType.DateTime).Value = (object)prisoner.DOB ?? DBNull.Value;
                            cmd.Parameters.Add("@entry", SqlDbType.DateTime).Value = (object)prisoner.EntryDate ?? DBNull.Value;
                            cmd.Parameters.Add("@rel", SqlDbType.DateTime).Value = (object)prisoner.ReleaseDate ?? DBNull.Value;
                            cmd.Parameters.Add("@st", SqlDbType.NVarChar).Value = (object)prisoner.Status ?? DBNull.Value;
                            cmd.Parameters.Add("@cid", SqlDbType.Int).Value = (object)prisoner.CellID ?? DBNull.Value;

                            prisonerId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        if (prisoner.Crimes != null)
                        {
                            string crimeQuery = @"INSERT INTO Crimes(PrisonerID, CrimeType, Description, CrimeDate, SentenceYears)
                                                 VALUES(@PID,@CrimeType,@Desc,@CrimeDate,@Years)";
                            foreach (var crime in prisoner.Crimes)
                            {
                                using (SqlCommand cmdCrime = new SqlCommand(crimeQuery, con, tx))
                                {
                                    cmdCrime.Parameters.Add("@PID", SqlDbType.Int).Value = prisonerId;
                                    cmdCrime.Parameters.Add("@CrimeType", SqlDbType.NVarChar).Value = (object)crime.CrimeType ?? DBNull.Value;
                                    cmdCrime.Parameters.Add("@Desc", SqlDbType.NVarChar).Value = (object)crime.Description ?? DBNull.Value;
                                    cmdCrime.Parameters.Add("@CrimeDate", SqlDbType.DateTime).Value = (object)crime.CrimeDate ?? DBNull.Value;
                                    cmdCrime.Parameters.Add("@Years", SqlDbType.Int).Value = crime.SentenceYears;
                                    cmdCrime.ExecuteNonQuery();
                                }
                            }
                        }

                        if (prisoner.Transfers != null)
                        {
                            string transferQuery = @"INSERT INTO Transfers(PrisonerID, FromPrison, ToPrison, TransferDate, ApprovedBy)
                                                    VALUES(@PID,@From,@To,@Date,@Approved)";
                            foreach (var tr in prisoner.Transfers)
                            {
                                using (SqlCommand cmdTr = new SqlCommand(transferQuery, con, tx))
                                {
                                    cmdTr.Parameters.Add("@PID", SqlDbType.Int).Value = prisonerId;
                                    cmdTr.Parameters.Add("@From", SqlDbType.NVarChar).Value = (object)tr.FromPrison ?? DBNull.Value;
                                    cmdTr.Parameters.Add("@To", SqlDbType.NVarChar).Value = (object)tr.ToPrison ?? DBNull.Value;
                                    cmdTr.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object)tr.TransferDate ?? DBNull.Value;
                                    cmdTr.Parameters.Add("@Approved", SqlDbType.NVarChar).Value = (object)tr.ApprovedBy ?? DBNull.Value;
                                    cmdTr.ExecuteNonQuery();
                                }
                            }
                        }

                        if (prisoner.Visits != null)
                        {
                            string visitQuery = @"INSERT INTO Visits(PrisonerID, VisitorID, VisitDate, Purpose, ApprovedByStaffID)
                                                 VALUES(@PID,@VID,@Date,@Purpose,@StaffID)";
                            foreach (var v in prisoner.Visits)
                            {
                                using (SqlCommand cmdV = new SqlCommand(visitQuery, con, tx))
                                {
                                    cmdV.Parameters.Add("@PID", SqlDbType.Int).Value = prisonerId;
                                    cmdV.Parameters.Add("@VID", SqlDbType.Int).Value = (object)v.VisitorID ?? DBNull.Value;
                                    cmdV.Parameters.Add("@Date", SqlDbType.DateTime).Value = (object)v.VisitDate ?? DBNull.Value;
                                    cmdV.Parameters.Add("@Purpose", SqlDbType.NVarChar).Value = (object)v.Purpose ?? DBNull.Value;
                                    cmdV.Parameters.Add("@StaffID", SqlDbType.Int).Value = (object)v.ApprovedByStaffID ?? DBNull.Value;
                                    cmdV.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
        public void AddTransfer(Transfer transfer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Transfers (PrisonerID, FromPrison, ToPrison, TransferDate, ApprovedBy)
                                 VALUES (@PrisonerID, @FromPrison, @ToPrison, @TransferDate, @ApprovedBy)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PrisonerID", transfer.PrisonerID);
                    cmd.Parameters.AddWithValue("@FromPrison", transfer.FromPrison);
                    cmd.Parameters.AddWithValue("@ToPrison", transfer.ToPrison);
                    cmd.Parameters.AddWithValue("@TransferDate", transfer.TransferDate);
                    cmd.Parameters.AddWithValue("@ApprovedBy", transfer.ApprovedBy);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void UpdatePrisonerWithCrimes(PrisonerModel prisoner, List<Crime> crimes)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        string updateQuery = @"UPDATE Prisoners
                                               SET FirstName=@FirstName, LastName=@LastName, Gender=@Gender, CellID=@CellID
                                               WHERE PrisonerID=@ID";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, con, tx))
                        {
                            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = (object)prisoner.FirstName ?? DBNull.Value;
                            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = (object)prisoner.LastName ?? DBNull.Value;
                            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = (object)prisoner.Gender ?? DBNull.Value;
                            cmd.Parameters.Add("@CellID", SqlDbType.Int).Value = (object)prisoner.CellID ?? DBNull.Value;
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = prisoner.PrisonerID;
                            cmd.ExecuteNonQuery();
                        }

                        string deleteCrimes = "DELETE FROM Crimes WHERE PrisonerID=@ID";
                        using (SqlCommand cmd = new SqlCommand(deleteCrimes, con, tx))
                        {
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = prisoner.PrisonerID;
                            cmd.ExecuteNonQuery();
                        }

                        if (crimes != null)
                        {
                            string crimeQuery = @"INSERT INTO Crimes(PrisonerID, CrimeType, Description, CrimeDate, SentenceYears)
                                                 VALUES(@PID, @CrimeType, @Desc, @CrimeDate, @Years)";
                            foreach (var crime in crimes)
                            {
                                using (SqlCommand cmdCrime = new SqlCommand(crimeQuery, con, tx))
                                {
                                    cmdCrime.Parameters.Add("@PID", SqlDbType.Int).Value = prisoner.PrisonerID;
                                    cmdCrime.Parameters.Add("@CrimeType", SqlDbType.NVarChar).Value = (object)crime.CrimeType ?? DBNull.Value;
                                    cmdCrime.Parameters.Add("@Desc", SqlDbType.NVarChar).Value = (object)crime.Description ?? DBNull.Value;
                                    cmdCrime.Parameters.Add("@CrimeDate", SqlDbType.DateTime).Value = (object)crime.CrimeDate ?? DBNull.Value;
                                    cmdCrime.Parameters.Add("@Years", SqlDbType.Int).Value = crime.SentenceYears;
                                    cmdCrime.ExecuteNonQuery();
                                }
                            }
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeletePrisonerCascade(int prisonerId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlTransaction tx = con.BeginTransaction())
                {
                    try
                    {
                        string[] deleteQueries = {
                            "DELETE FROM Visits WHERE PrisonerID=@ID",
                            "DELETE FROM Transfers WHERE PrisonerID=@ID",
                            "DELETE FROM Crimes WHERE PrisonerID=@ID",
                            "DELETE FROM Prisoners WHERE PrisonerID=@ID"
                        };

                        foreach (var query in deleteQueries)
                        {
                            using (SqlCommand cmd = new SqlCommand(query, con, tx))
                            {
                                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = prisonerId;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // --- FIXED SECTION ---
        public List<PrisonerModel> GetAllPrisoners(bool includeHistory = true)
        {
            List<PrisonerModel> prisoners = new List<PrisonerModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // 1️⃣ Get all prisoners
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Prisoners", con))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        prisoners.Add(new PrisonerModel
                        {
                            PrisonerID = Convert.ToInt32(dr["PrisonerID"]),
                            FirstName = dr["FirstName"]?.ToString(),
                            LastName = dr["LastName"]?.ToString(),
                            Gender = dr["Gender"]?.ToString(),
                            DOB = dr["DOB"] != DBNull.Value ? (DateTime?)dr["DOB"] : null,
                            EntryDate = dr["EntryDate"] != DBNull.Value ? (DateTime?)dr["EntryDate"] : null,
                            ReleaseDate = dr["ReleaseDate"] != DBNull.Value ? (DateTime?)dr["ReleaseDate"] : null,
                            Status = dr["Status"]?.ToString(),
                            CellID = dr["CellID"] != DBNull.Value ? (int?)dr["CellID"] : null,
                            Crimes = new List<Crime>(),
                            Transfers = new List<Transfer>(),
                            Visits = new List<Visit>()
                        });
                    }
                }

                if (includeHistory && prisoners.Count > 0)
                {
                    var prisonerDict = prisoners.ToDictionary(p => p.PrisonerID);

                    // 2️⃣ Load Crimes
                    using (SqlCommand cmdCrime = new SqlCommand("SELECT * FROM Crimes", con))
                    using (SqlDataReader drCrime = cmdCrime.ExecuteReader())
                    {
                        while (drCrime.Read())
                        {
                            int pid = Convert.ToInt32(drCrime["PrisonerID"]);
                            if (prisonerDict.ContainsKey(pid))
                            {
                                prisonerDict[pid].Crimes.Add(new Crime
                                {
                                    CrimeID = Convert.ToInt32(drCrime["CrimeID"]),
                                    PrisonerID = pid,
                                    CrimeType = drCrime["CrimeType"]?.ToString(),
                                    Description = drCrime["Description"]?.ToString(),
                                    CrimeDate = drCrime["CrimeDate"] != DBNull.Value ? (DateTime?)drCrime["CrimeDate"] : null,
                                    SentenceYears = drCrime["SentenceYears"] != DBNull.Value ? Convert.ToInt32(drCrime["SentenceYears"]) : 0
                                });
                            }
                        }
                    }

                    // 3️⃣ Load Transfers
                    using (SqlCommand cmdTr = new SqlCommand("SELECT * FROM Transfers", con))
                    using (SqlDataReader drTr = cmdTr.ExecuteReader())
                    {
                        while (drTr.Read())
                        {
                            int pid = Convert.ToInt32(drTr["PrisonerID"]);
                            if (prisonerDict.ContainsKey(pid))
                            {
                                prisonerDict[pid].Transfers.Add(new Transfer
                                {
                                    TransferID = Convert.ToInt32(drTr["TransferID"]),
                                    PrisonerID = pid,
                                    FromPrison = drTr["FromPrison"]?.ToString(),
                                    ToPrison = drTr["ToPrison"]?.ToString(),
                                    TransferDate = drTr["TransferDate"] != DBNull.Value ? (DateTime?)drTr["TransferDate"] : null,
                                    ApprovedBy = drTr["ApprovedBy"]?.ToString()
                                });
                            }
                        }
                    }

                    // 4️⃣ Load Visits
                    using (SqlCommand cmdVisit = new SqlCommand("SELECT * FROM Visits", con))
                    using (SqlDataReader drVisit = cmdVisit.ExecuteReader())
                    {
                        while (drVisit.Read())
                        {
                            int pid = Convert.ToInt32(drVisit["PrisonerID"]);
                            if (prisonerDict.ContainsKey(pid))
                            {
                                prisonerDict[pid].Visits.Add(new Visit
                                {
                                    VisitID = Convert.ToInt32(drVisit["VisitID"]),
                                    PrisonerID = pid,
                                    VisitorID = drVisit["VisitorID"] != DBNull.Value ? (int?)drVisit["VisitorID"] : null,
                                    VisitDate = drVisit["VisitDate"] != DBNull.Value ? (DateTime?)drVisit["VisitDate"] : null,
                                    Purpose = drVisit["Purpose"] != DBNull.Value ? drVisit["Purpose"].ToString() : null,
                                    ApprovedByStaffID = drVisit["ApprovedByStaffID"] != DBNull.Value ? (int?)drVisit["ApprovedByStaffID"] : null
                                });
                            }
                        }
                    }
                }
            }

            

            return prisoners;
        }

        public List<Transfer> GetAllTransfers()
        {

            List<Transfer> transfer = new List<Transfer>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmdTr = new SqlCommand("SELECT * FROM Transfers", con))
                using (SqlDataReader drTr = cmdTr.ExecuteReader())
                {
                    while (drTr.Read())
                    {
                            transfer.Add(new Transfer
                            {
                                TransferID = Convert.ToInt32(drTr["TransferID"]),
                                PrisonerID = Convert.ToInt32(drTr["PrisonerID"]),
                                FromPrison = drTr["FromPrison"]?.ToString(),
                                ToPrison = drTr["ToPrison"]?.ToString(),
                                TransferDate = drTr["TransferDate"] != DBNull.Value ? (DateTime?)drTr["TransferDate"] : null,
                                ApprovedBy = drTr["ApprovedBy"]?.ToString()
                            });
                       
                    }
                }
            }

            return transfer;
        }

    }
}