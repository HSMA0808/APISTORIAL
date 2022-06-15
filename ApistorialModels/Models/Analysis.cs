﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class Analysis : EntityBase
    {
        public AnalysisType analysisType { get; set; }
        public ResultType resultType { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public int Save(string connectionString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO Analysis (IdAnalysisType, IdResultType, Description, Code, CreateUser, CreateDate) 
                                VALUES (@IdAnalysis, @IdResultType, @Description, @Code, @CreateUser, @CreateDate)";
            cmd.Parameters.Add(new SqlParameter("@IdAnalysisType", analysisType.ID));
            cmd.Parameters.Add(new SqlParameter("@IdResultType", resultType.ID));
            cmd.Parameters.Add(new SqlParameter("@Description", Description));
            cmd.Parameters.Add(new SqlParameter("@Code", Code));
            var db = new DBConnection(connectionString);
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("select top(1) * from Analysis"));
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public void Update(string connectionString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE Analysis SET 
                                                IdAnalysis = @IdAnalysis
                                                ,IdResultType = @IdResultType
                                                ,Description = @Description
                                                ,Code = @Code
                                                ,CreateUser = @CreateUser
                                                ,CreateDate = @CreateDate) 
                                WHERE IdAnalysis = @IdAnalysis";
            cmd.Parameters.Add(new SqlParameter("@IdAnalysis", ID));
            cmd.Parameters.Add(new SqlParameter("@IdAnalysisType", analysisType.ID));
            cmd.Parameters.Add(new SqlParameter("@IdResultType", resultType.ID));
            cmd.Parameters.Add(new SqlParameter("@Description", Description));
            cmd.Parameters.Add(new SqlParameter("@Code", Code));
            var db = new DBConnection(connectionString);
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("select top(1) * from Analysis"));
        }

        public List<Analysis> ToList(string connectionString, int Top =0)
        {
            var AnalysisList = new List<Analysis>();
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP(@Top) * FROM Analysis";
            }
            else
            {
                cmd.CommandText = @"SELECT * FROM Analysis";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                AnalysisList.Add(new Analysis()
                {
                    ID = int.Parse(row["ID"].ToString())
                    ,analysisType = new AnalysisType() { ID = int.Parse(row["IdAnalysisType"].ToString()) }
                    ,resultType = new ResultType() { ID = int.Parse(row["IdResultType"].ToString()) }
                    ,Description = row["Description"].ToString()
                    ,Code  = row["Code"].ToString()
                });
            }
            return AnalysisList;
        }

    }
}
