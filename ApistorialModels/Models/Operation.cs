using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class Operation : EntityBase
    {
        public OperationType operationType { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public Operation()
        {
            operationType = new OperationType();
        }
        public List<Operation> TolList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var operationType_ = new OperationType();
            var operationList = new List<Operation>();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP("+Top+") FROM OPERATION";
            }
            else
            {
                cmd.CommandText = @"SELECT * FROM OPERATION";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                operationList.Add(new Operation()
                {
                    ID = int.Parse(row["ID"].ToString()),
                    operationType = operationType_.Find(int.Parse(row["IDOPERATION_TYPE"].ToString()), connectionString),
                    Description = row["DESCRIPTION"].ToString(),
                    Code = row["CODE"].ToString(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())
                });
            }
            return operationList;
        }

        public Operation Find(int Id, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var operationType_ = new OperationType();
            var operationList = new List<Operation>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM OPERATION WHERE IDOPERATION = "+Id+"";
            var ds = db.ExtractDataSet(cmd);
            return new Operation()
            {
                ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString()),
                operationType = operationType_.Find(int.Parse(ds.Tables[0].Rows[0]["IDOPERATION_TYPE"].ToString()), connectionString),
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
