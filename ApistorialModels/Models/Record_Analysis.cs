using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class Record_Analysis : EntityBase
    {
        public Record record { get; set; }
        public Analysis analysis { get; set; }
        public bool PublicResults { get; set; }
        public string Results { get; set; }

        public Record_Analysis()
        { 
            record = new Record();
            analysis = new Analysis();
        }
        public int Save(string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oAnalysis = new Analysis();
            var cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO RECORD_ANALYSIS
                                            (IDRECORD, IDNALYSIS, PUBLIC_RESULTS, RESULTS, CREATE_USER, CREATE_DATE)
                                            VALUES
                                            (@IDRECORD, @IDNALYSIS, @PUBLIC_RESULTS, @RESULTS, @CREATE_USER, @CREATE_DATE)";
            cmd.Parameters.Add(new SqlParameter("@IDRECORD", record.ID));
            cmd.Parameters.Add(new SqlParameter("@IDANALYSIS", analysis.ID));
            cmd.Parameters.Add(new SqlParameter("@PUBLIC_RESULTS", PublicResults));
            Results = PublicResults? Results : Token(Results);
            cmd.Parameters.Add(new SqlParameter("@RESULTS", Token(Results)));
            cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            cmd.Parameters.Add(new SqlParameter("@CREATE_USER", CreateUser));
            cmd.Parameters.Add(new SqlParameter("@UPDATE_DATE", UpdateDate));
            cmd.Parameters.Add(new SqlParameter("@UPDATE_USER", UpdateUser));
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("SELECT TOP(1) FROM RECORD_ANALYSIS ORDER BY CREATE_DATE"));
            return int.Parse(ds.Tables[0].Rows[0]["IDRECORD_ANALYSIS"].ToString());
        }

        public Record_Analysis Find(int idRecord_Analysis, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oAnalysis = new Analysis();
            var cmd = new SqlCommand("SELECT * FROM RECORD_ANALYSIS WHERE IDRECORD_ANALYSIS = @IDRECORD_ANALYSIS");
            cmd.Parameters.Add(new SqlParameter("@IDRECORD_ANALYSIS", idRecord_Analysis));
            var ds = db.ExtractDataSet(cmd);
            return new Record_Analysis() {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDRECORD_ANALYSIS"].ToString()),
                record = record.Find(int.Parse(ds.Tables[0].Rows[0]["IDRECORD"].ToString()), connectionString),
                analysis = analysis.Find(int.Parse(ds.Tables[0].Rows[0]["IDANALYSIS"].ToString()), connectionString),
                PublicResults = bool.Parse(ds.Tables[0].Rows[0]["PUBLIC_RESULTS"].ToString()),
                Results = ds.Tables[0].Rows[0]["RESULTS"].ToString(),
                CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString())
            };
        }

        public List<Record_Analysis> ToList(string connectionString, int Top = 0)
        {
            var RecordAnalysisList = new List<Record_Analysis>();
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oAnalysis = new Analysis();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd = new SqlCommand("SELECT TOP(@TOP) * FROM RECORD_ANALYSIS");
                cmd.Parameters.Add(new SqlParameter("@TOP", Top));
            }
            else
            {
                cmd = new SqlCommand("SELECT * FROM RECORD_ANALYSIS");
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordAnalysisList.Add(new Record_Analysis()
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["IDRECORD_ANALYSIS"].ToString()),
                    record = record.Find(int.Parse(ds.Tables[0].Rows[0]["IDRECORD"].ToString()), connectionString),
                    analysis = analysis.Find(int.Parse(ds.Tables[0].Rows[0]["IDANALYSIS"].ToString()), connectionString),
                    PublicResults = bool.Parse(ds.Tables[0].Rows[0]["PUBLIC_RESULTS"].ToString()),
                    Results = ds.Tables[0].Rows[0]["RESULTS"].ToString(),
                    CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                    UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString())
                });
            }
            return RecordAnalysisList;
        }

        private string Token(string value)
        {
            return "";
        }
    }
}
