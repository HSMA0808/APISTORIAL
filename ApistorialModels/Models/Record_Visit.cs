using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class Record_Visit : EntityBase
    {
        public Record record { get; set; }
        public Doctor doctor { get; set; }
        public Specialtys specialty { get; set; }
        public string Observations { get; set; }
        public string Indications { get; set; }
        public DateTime VisitDate { get; set; }

        public Record_Visit()
        {
            record = new Record();
            doctor = new Doctor();
            specialty = new Specialtys();
        }
        public int Save(string connectionString)
        { 
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"EXEC INSERT_RECORDVISIT @IDRECORD, @IDDOCTOR, @IDSPECIALTY, @OBSERVATIONS, @INDICATIONS, @VISIT_DATE, @CREATE_USER, @CREATE_DATE)";

            cmd.Parameters.Add(new SqlParameter("@IDRECORD", record.ID));
            cmd.Parameters.Add(new SqlParameter("@IDDOCTOR", doctor.ID));
            cmd.Parameters.Add(new SqlParameter("@IDSPECIALTY", specialty.ID));
            cmd.Parameters.Add(new SqlParameter("@OBSERVATIONS", Observations));
            cmd.Parameters.Add(new SqlParameter("@INDICATIONS", Indications));
            cmd.Parameters.Add(new SqlParameter("@VISIT_DATE", VisitDate));
            cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            cmd.Parameters.Add(new SqlParameter("@CREATE_USER", CreateUser));
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand(@"SELECT TOP(1) * FROM RECORD_VISITS ORDER BY CREATE_DATE DESC"));
            return int.Parse(ds.Tables[0].Rows[0]["IDRECORD_VISITS"].ToString());
        }

        public List<Record_Visit> Find(int idRecord, string connectionString)
        { 
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oDoctor = new Doctor();
            var oSpecialty = new Specialtys();
            var RecordVisitsList = new List<Record_Visit>();
            var cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM RECORD_VISITS WHERE IDRECORD = "+idRecord+"";
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordVisitsList.Add(new Record_Visit()
                {
                    ID = int.Parse(row["IDRECORD_VISITS"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    doctor = oDoctor.Find(int.Parse(row["IDDOCTOR"].ToString()), connectionString),
                    specialty = oSpecialty.Find(int.Parse(row["IDSPECIALTY"].ToString()), connectionString),
                    Observations = row["OBSERVATIONS"].ToString(),
                    Indications = row["INDICATIONS"].ToString(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())

                });
            }
            return RecordVisitsList;
        }

        public List<Record_Visit> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oDoctor = new Doctor();
            var oSpecialty = new Specialtys();
            var RecordVisitsList = new List<Record_Visit>();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = "SELECT TOP("+Top+") * FROM RECORD_VISITS ORDER BY CREATE_DATE DESC";
            }
            else
            {
                cmd.CommandText = "SELECT * FROM RECORD_VISITS ORDER BY CREATE_DATE DESC";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordVisitsList.Add(new Record_Visit()
                {
                    ID = int.Parse(row["IDRECORD_VISITS"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    doctor = oDoctor.Find(int.Parse(row["IDDOCTOR"].ToString()), connectionString),
                    specialty = oSpecialty.Find(int.Parse(row["IDSPECIALTY"].ToString()), connectionString),
                    Observations = row["OBSERVATIONS"].ToString(),
                    Indications = row["INDICATIONS"].ToString(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())

                });
            }
            return RecordVisitsList;
        }
    }
}
