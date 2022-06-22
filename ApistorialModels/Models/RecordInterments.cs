using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class RecordInterments : EntityBase
    {
        public Record record { get; set; }
        public MedicalCenter medicalCenter { get; set; }
        public DateTime IntermentDate { get; set; }
        public string Reason { get; set; }

        public RecordInterments()
        {
            record = new Record();
            medicalCenter = new MedicalCenter();
        }
        public int Save(string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"EXEC INSERT_RECORDINTERMENTS @IDRECORD, @IDMEDICAL_CENTER, @INTERMENTDATE, @REASON, @CREATE_USER, @CREATE_DATE)";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD", record.ID));
            cmd.Parameters.Add(new SqlParameter("@IDMEDICAL_CENTER", medicalCenter.ID));
            cmd.Parameters.Add(new SqlParameter("@INTERMENTDATE", IntermentDate));
            cmd.Parameters.Add(new SqlParameter("@REASON", Reason));
            cmd.Parameters.Add(new SqlParameter("@CREATE_USER", CreateUser));
            cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("SELECT TOP(1) IDRECORD_INTERMENT FROM RECORD_INTERMENTS ORDER BY CREATE_DATE DESC"));
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public List<RecordInterments> Find(int idRecordInterments, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oMedicalCenter = new MedicalCenter();
            var oRecord = new Record();
            var RecordIntermentsList = new List<RecordInterments>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM RECORD_INTERMENTS WHERE IDRECORD = "+idRecordInterments+"";
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordIntermentsList.Add(new RecordInterments()
                {
                    ID = int.Parse(row["IDRECORD_INTERMENT"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    medicalCenter = oMedicalCenter.Find(int.Parse(row["IDMEDICAL_CENTER"].ToString()), connectionString),
                    IntermentDate = DateTime.Parse(row["INTERMENTDATE"].ToString()),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordIntermentsList;
        }

        public List<RecordInterments> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var RecordIntermentsList = new List<RecordInterments>();
            var oMedicalCenter = new MedicalCenter();
            var oRecord = new Record();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP("+Top+") * FROM RECORD_INTERMENTS";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordIntermentsList.Add(new RecordInterments()
                {
                    ID = int.Parse(row["IDRECORD_INTERMENT"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    medicalCenter = oMedicalCenter.Find(int.Parse(row["IDMEDICAL_CENTER"].ToString()), connectionString),
                    IntermentDate = DateTime.Parse(row["INTERMENTDATE"].ToString()),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordIntermentsList;
        }
    }
}
