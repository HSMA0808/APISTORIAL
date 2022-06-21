using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class AnalysisType : ActionTypes
    {
        public AnalysisType Find(int idAnalysisType, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM ANALYSIS_TYPE WHERE IDANALYSIS_TYPE = "+ idAnalysisType + "";
            var ds = db.ExtractDataSet(cmd);
            return new AnalysisType()
            {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDANALYSIS_TYPE"].ToString()),
                Description = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString(),
                Code = ds.Tables[0].Rows[0]["CODE"].ToString(),
                CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString())
            };
        }
    }
}
