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

        public int Save(string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO RECORD_OPERATIONS 
                                            (IDRECORD,
                                             IDOPERATION,
                                             IDDOCTOR,
                                             OPERATIONDATE,
                                             CREATE_USER,
                                             CREATE_DATE)
                                        VALUES
                                            (@IDRECORD,
                                             @IDOPERATION,
                                             @IDDOCTOR,
                                             @OPERATIONDATE,
                                             @CREATE_USER,
                                             @CREATE_DATE)";
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

        public Record_Operation Find(int id, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oOperation = new Operation();
            var oDoctor = new Doctor();
            var cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM RECORD_OPERATIONS WHERE IDRECORD_OPERATIONS = @IDRECORD_OPERATIONS";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD_OPERATIONS", id));
            var ds = db.ExtractDataSet(cmd);
            return new Record_Operation() {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDRECORD_OPERATIONS"].ToString()),
                record = oRecord.Find(int.Parse(ds.Tables[0].Rows[0]["IDRECORD"].ToString()), connectionString),
                operation = oOperation.Find(int.Parse(ds.Tables[0].Rows[0]["IDOPERATION"].ToString()), connectionString),
                doctor = oDoctor.Find(int.Parse(ds.Tables[0].Rows[0]["IDDOCTOR"].ToString()), connectionString),
                OperationDate = DateTime.Parse(ds.Tables[0].Rows[0]["OPERATIONDATE"].ToString()),
                CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString())
            };
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
                cmd.CommandText = "SELECT TOP(@TOP) * FROM RECORD_OPERATIONS";
                cmd.Parameters.Add(new SqlParameter("@TOP", Top));
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
                    ID = int.Parse(ds.Tables[0].Rows[0]["IDRECORD_OPERATIONS"].ToString()),
                    record = oRecord.Find(int.Parse(ds.Tables[0].Rows[0]["IDRECORD"].ToString()), connectionString),
                    operation = oOperation.Find(int.Parse(ds.Tables[0].Rows[0]["IDOPERATION"].ToString()), connectionString),
                    doctor = oDoctor.Find(int.Parse(ds.Tables[0].Rows[0]["IDDOCTOR"].ToString()), connectionString),
                    OperationDate = DateTime.Parse(ds.Tables[0].Rows[0]["OPERATIONDATE"].ToString()),
                    CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                    UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString())
                });
            }
            return RecordOperationList;
        }
    }
}
