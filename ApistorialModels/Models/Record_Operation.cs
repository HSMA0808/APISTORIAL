using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class Record_Operation : EntityBase
    {
        public Record record { get; set; }
        public Operation operation { get; set; }
        public Doctor doctor { get; set; }
        public DateTime OperationDate { get; set; }

        public Record_Operation()
        {
            record = new Record();
            operation = new Operation();
            doctor = new Doctor();
        }
        public int Save(string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"EXEC INSER_RECORDOPERATION @IDRECORD, @IDOPERATION, @IDDOCTOR, @OPERATIONDATE, @CREATE_USER, @CREATE_DATE)";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD", record.ID));
            cmd.Parameters.Add(new SqlParameter("@IDOPERATION", operation.ID));
            cmd.Parameters.Add(new SqlParameter("@IDDOCTOR", doctor.ID));
            cmd.Parameters.Add(new SqlParameter("@OEPRATIONDATE", OperationDate));
            cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            cmd.Parameters.Add(new SqlParameter("@CREATE_USER", CreateUser));
            db.ExecuteCommand(cmd);
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT TOP(1) FROM RECORD_OPERATIONS ORDER BY CREATE_DATE DESC";
            var ds = db.ExtractDataSet(cmd);
            return int.Parse(ds.Tables[0].Rows[0]["IDRECORD_OPERATIONs"].ToString());
        }

        public List<Record_Operation> Find(int idRecord, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oOperation = new Operation();
            var oDoctor = new Doctor();
            var RecordOperationList = new List<Record_Operation>();
            var cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM RECORD_OPERATIONS WHERE IDRECORD = "+idRecord+"";
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordOperationList.Add(new Record_Operation()
                {
                    ID = int.Parse(row["IDRECORD_OPERATIONS"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    operation = oOperation.Find(int.Parse(row["IDOPERATION"].ToString()), connectionString),
                    doctor = oDoctor.Find(int.Parse(row["IDDOCTOR"].ToString()), connectionString),
                    OperationDate = DateTime.Parse(row["OPERATIONDATE"].ToString()),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordOperationList;
        }

        public List<Record_Operation> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var RecordOperationList = new List<Record_Operation>();
            var oRecord = new Record();
            var oOperation = new Operation();
            var oDoctor = new Doctor();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = "SELECT TOP("+Top+") * FROM RECORD_OPERATIONS";
            }
            else
            {
                cmd.CommandText = "SELECT * FROM RECORD_OPERATIONS";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordOperationList.Add(new Record_Operation()
                {
                    ID = int.Parse(row["IDRECORD_OPERATIONS"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    operation = oOperation.Find(int.Parse(row["IDOPERATION"].ToString()), connectionString),
                    doctor = oDoctor.Find(int.Parse(row["IDDOCTOR"].ToString()), connectionString),
                    OperationDate = DateTime.Parse(row["OPERATIONDATE"].ToString()),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordOperationList;
        }
    }
}
