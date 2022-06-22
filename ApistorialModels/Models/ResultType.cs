using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class ResultType : ActionTypes
    {
        public ResultType Find(int idResultType, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM RESULT_TYPE WHERE IDRESULT_TYPE = "+idResultType+"";
            var ds = db.ExtractDataSet(cmd);
            return new ResultType()
            {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDRESULT_TYPE"].ToString()),
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
