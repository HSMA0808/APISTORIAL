using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace ApistorialModels.Models
{
    public class RecordVaccines : EntityBase
    {
        public Record record { get; set; }
        public List<NameValue> NvVaccine { get; set; }
        public RecordVaccines()
        {
            record = new Record();
        }

        public void Save(string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            foreach (NameValue nv in NvVaccine)
            {
                cmd.CommandText = @"EXEC INSERT_RECORDVACCINES @IDRECORD, @NVVACCINE, @CREATE_USER, @CREATE_DATE)";
                cmd.Parameters.Add(new SqlParameter("@IDRECORD", record.ID));
                cmd.Parameters.Add(new SqlParameter("@NVVACCINE", nv.ID));
                cmd.Parameters.Add(new SqlParameter("@CREATE_USER", CreateUser));
                cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            }
            db.ExecuteCommand(cmd);
        }

        public List<RecordVaccines> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oNameValue = new NameValue();
            var RecordVaccinesList = new List<RecordVaccines>();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP(@TOP) * FROM RECORD_VACCINES ORDER BY CREATE_DATE DESC";
                cmd.Parameters.Add(new SqlParameter("@TOP", Top));
            }
            else
            {
                cmd.CommandText = @"SELECT * FROM RECORD_VACCINES ORDER BY CREATE_DATE DESC";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordVaccinesList.Add(new RecordVaccines() { 
                    ID = int.Parse(row["IDRECORD_VACCINES"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    NvVaccine = oNameValue.ToList(connectionString).Where(x=>x.GroupName == "VACCINE_GROUP").ToList(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordVaccinesList;
        }

        public List<RecordVaccines> Find(int IDRecord, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oNameValue = new NameValue();
            var RecordVaccinesList = new List<RecordVaccines>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM RECORD_VACCINES WHERE IDRECORD = @IDRECORD";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD", IDRecord));
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordVaccinesList.Add(new RecordVaccines()
                {
                    ID = int.Parse(row["IDRECORD_VACCINES"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    NvVaccine = oNameValue.ToList(connectionString).Where(x => x.GroupName == "VACCINE_GROUP").ToList(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordVaccinesList;
        }
    }
}
