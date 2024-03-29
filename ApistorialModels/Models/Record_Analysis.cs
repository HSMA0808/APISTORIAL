﻿using System;
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
            cmd.CommandText = @"EXEC INSERT_RECORDANALYSIS @IDRECORD, @IDNALYSIS, @PUBLIC_RESULTS, @RESULTS, @CREATE_USER, @CREATE_DATE)";
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

        public List<Record_Analysis> Find(int idRecord, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oRecord = new Record();
            var oAnalysis = new Analysis();
            var RecordAnalysisList = new List<Record_Analysis>();
            var cmd = new SqlCommand("SELECT * FROM RECORD_ANALYSIS WHERE IDRECORD = "+ idRecord + "");
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                RecordAnalysisList.Add(new Record_Analysis()
                {
                    ID = int.Parse(row["IDRECORD_ANALYSIS"].ToString()),
                    record = record.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    analysis = analysis.Find(int.Parse(row["IDANALYSIS"].ToString()), connectionString),
                    PublicResults = bool.Parse(row["PUBLIC_RESULTS"].ToString()),
                    Results = row["RESULTS"].ToString(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return RecordAnalysisList;
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
                cmd = new SqlCommand("SELECT TOP("+Top+") * FROM RECORD_ANALYSIS");
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
                    ID = int.Parse(row["IDRECORD_ANALYSIS"].ToString()),
                    record = record.Find(int.Parse(row["IDRECORD"].ToString()), connectionString),
                    analysis = analysis.Find(int.Parse(row["IDANALYSIS"].ToString()), connectionString),
                    PublicResults = bool.Parse(row["PUBLIC_RESULTS"].ToString()),
                    Results = row["RESULTS"].ToString(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
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
