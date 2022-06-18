using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class OperationType : ActionTypes
    {

        public OperationType Find(int idOperationType, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM OPERATION_TYPE WHERE IDOPERATION_TYPE = @IDOPERATION_TYPE";
            cmd.Parameters.Add(new SqlParameter("@IDOPERATION_TYPE", idOperationType));
            var ds = db.ExtractDataSet(cmd);
            return new OperationType()
            {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDOPERATION_TYPE"].ToString()),
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
