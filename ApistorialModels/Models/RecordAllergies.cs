using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ApistorialModels.Models
{
    public class RecordAllergies : EntityBase
    {
        public Record record { get; set; }
        public List<NameValue> NvAllergies { get; set; }
        public RecordAllergies()
        {
            record = new Record();
        }

        public void Save(string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO RECORD_ALLERGIES
                                            (IDRECORD_ALLERGIES,
                                             IDRECORD,
                                             NVALLERGIES,
                                             CREATE_USER,
                                             CREATE_DATE)
                                             VALUES
                                             (@IDRECORD_ALLERGIES,
                                             @IDRECORD,
                                             @NVALLERGIES,
                                             @CREATE_USER,
                                             @CREATE_DATE)";
            db.ExecuteCommand(cmd);
        }

        public List<RecordAllergies> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oNameValue = new NameValue();
            var RecordAllergiesList = new List<RecordAllergies>();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP(@TOP) * FROM RECORD_ALLERGIES ORDER BY CREATE_DATE DESC";
                cmd.Parameters.Add(new SqlParameter("@TOP", Top));
            }
            else
            {
                cmd.CommandText = @"SELECT * FROM RECORD_ALLERGIES ORDER BY CREATE_DATE DESC";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordAllergiesList.Add(new RecordAllergies()
                {
                    ID = int.Parse(row["IDRECORD_VACCINES"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    NvAllergies = oNameValue.ToList(connectionString).Where(x => x.GroupName == "ALLERGIES_GROUP").ToList(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordAllergiesList;
        }

        public List<RecordAllergies> Find(int IDRecord, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oNameValue = new NameValue();
            var RecordAllergiesList = new List<RecordAllergies>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM RECORD_ALLERGIES WHERE IDRECORD = @IDRECORD";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD", IDRecord));
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordAllergiesList.Add(new RecordAllergies()
                {
                    ID = int.Parse(row["IDRECORD_VACCINES"].ToString()),
                    record = oRecord.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    NvAllergies = oNameValue.ToList(connectionString).Where(x => x.GroupName == "VACCINE_GROUP").ToList(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordAllergiesList;
        }
    }
}
