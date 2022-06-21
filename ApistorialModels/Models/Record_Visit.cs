using System;
using System.Collections.Generic;
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
            cmd.CommandText = @"INSERT INTO RECORD_VISITS
                                            (IDRECORD, IDDOCTOR, IDSPECIALTY, OBSERVATIONS, INDICATIONS, VISIT_DATE
                                            , CREATE_USER, CREATE_DATE, UPDATE_USER, UPDATE_DATE)
                                            VALUES
                                            (@IDRECORD, @IDDOCTOR, @IDSPECIALTY, @OBSERVATIONS, @INDICATIONS, @VISIT_DATE
                                            , @CREATE_USER, @CREATE_DATE, @UPDATE_USER, @UPDATE_DATE)";

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

        public Record_Visit Find(int idRecord_Visits, string connectionString)
        { 
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oDoctor = new Doctor();
            var oSpecialty = new Specialtys();
            var cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM RECORD_VISITS WHERE IDRECORD_VISITS = @IDRECORD_VISITS";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD_VISITS", idRecord_Visits));
            var ds = db.ExtractDataSet(cmd);
            return new Record_Visit() {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDRECORD_VISITS"].ToString()),
                record = oRecord.Find(int.Parse(ds.Tables[0].Rows[0]["IDRECORD"].ToString()), connectionString),
                doctor = oDoctor.Find(int.Parse(ds.Tables[0].Rows[0]["IDDOCTOR"].ToString()), connectionString),
                specialty = oSpecialty.Find(int.Parse(ds.Tables[0].Rows[0]["IDSPECIALTY"].ToString()), connectionString),
                Observations = ds.Tables[0].Rows[0]["OBSERVATIONS"].ToString(),
                Indications = ds.Tables[0].Rows[0]["INDICATIONS"].ToString(),
                CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString())

            };
        }
    }
}
