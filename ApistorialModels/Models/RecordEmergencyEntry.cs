using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class RecordEmergencyEntry : EntityBase
    {
        public Record record { get; set; }
        public MedicalCenter medicalCenter { get; set; }
        public DateTime IntermentDate { get; set; }
        public string Reason { get; set; }

        public RecordEmergencyEntry()
        {
            record = new Record();
            medicalCenter = new MedicalCenter();
        }

        public int Save(string connectionString)
        { 
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"EXEC INSERT_RECORDEMERGENCYENTRY @IDRECORD, @IDMEDICAL_CENTER, @INTERMENTDATE, @REASON, @CREATE_USER, @CREATE_DATE)";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD", record.ID));
            cmd.Parameters.Add(new SqlParameter("@IDMEDICAL_CENTER", medicalCenter.ID));
            cmd.Parameters.Add(new SqlParameter("@INTERMENTDATE", IntermentDate));
            cmd.Parameters.Add(new SqlParameter("@REASON", Reason));
            cmd.Parameters.Add(new SqlParameter("@CREATE_USER",CreateUser));
            cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("SELECT TOP(1) IDRECORD_EMERGENCYENTRY FROM RECORD_EMERGENCYENTRY ORDER BY CREATE_DATE DESC"));
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public RecordEmergencyEntry Find(int idRecordEmergencyEntry, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oMedicalCenter = new MedicalCenter();
            var oRecord = new Record();
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM RECORD_EMERGENCYENTRY WHERE IDRECORD_EMERGENCYENTRY = @IDRECORD_EMERGENCYENTRY";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD_EMERGENCYENTRY", idRecordEmergencyEntry));
            var ds = db.ExtractDataSet(cmd);
            return new RecordEmergencyEntry() {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDRECORD_EMERGENCYENTRY"].ToString()),
                record = oRecord.Find(int.Parse(ds.Tables[0].Rows[0]["IDRECORD"].ToString()), connectionString),
                medicalCenter = oMedicalCenter.Find(int.Parse(ds.Tables[0].Rows[0]["IDMEDICAL_CENTER"].ToString()), connectionString),
                IntermentDate = DateTime.Parse(ds.Tables[0].Rows[0]["INTERMENTDATE"].ToString()),
                CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString())
            };
        }

        public List<RecordEmergencyEntry> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var RecordEmergencyEntryList = new List<RecordEmergencyEntry>();
            var oMedicalCenter = new MedicalCenter();
            var oRecord = new Record();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP(@TOP) * FROM RECORD_EMERGENCYENTRY";
                cmd.Parameters.Add(new SqlParameter("@top", Top));
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordEmergencyEntryList.Add(new RecordEmergencyEntry() {
                    ID = int.Parse(row["IDRECORD_EMERGENCYENTRY"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    medicalCenter = oMedicalCenter.Find(int.Parse(row["IDMEDICAL_CENTER"].ToString()), connectionString),
                    IntermentDate = DateTime.Parse(row["INTERMENTDATE"].ToString()),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });  
            }
            return RecordEmergencyEntryList;
        }
    }
}
