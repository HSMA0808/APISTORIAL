using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class NameValue : EntityBase
    {
        public int IdGroup { get; set; }
        public string Description { get; set; }
        public string CustomString1 { get; set; }
        public string CustomString2 { get; set; }
        public int CustomInt1 { get; set; }
        public int CustomInt2 { get; set; }
        public string GroupName { get; set; }

        public NameValue Find(int IdNameValue, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM NAMEVALUE WHERE IDNAMEVALUE = "+IdNameValue+"";
            var ds = db.ExtractDataSet(cmd);
            return new NameValue() {
                ID = int.Parse(ds.Tables[0].Rows[0]["IDNAMEVALUE"].ToString()),
                Description = ds.Tables[0].Rows[0]["DESCRIPTION"].ToString(),
                CustomString1 = ds.Tables[0].Rows[0]["CUSTOMSTRING1"].ToString(),
                CustomString2 = ds.Tables[0].Rows[0]["CUSTOMSTRING2"].ToString(),
                CustomInt1 = int.Parse(ds.Tables[0].Rows[0]["CUSTOMINT1"].ToString()),
                CustomInt2 = int.Parse(ds.Tables[0].Rows[0]["CUSTOMINT2"].ToString()),
                CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString()),
            };
        }

        public List<NameValue> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            var nameValueList = new List<NameValue>(); 
            if (Top > 0)
            {
                cmd.CommandText = "SELECT TOP(" + Top + ") * FROM NAMEVALUE";
            }
            else
            {
                cmd.CommandText = "SELECT * FROM NAMEVALUE";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                nameValueList.Add(new NameValue() {
                    ID = int.Parse(row["IDNAMEVALUE"].ToString()),
                    Description = row["DESCRIPTION"].ToString(),
                    CustomString1 = row["CUSTOMSTRING1"].ToString(),
                    CustomString2 = row["CUSTOMSTRING2"].ToString(),
                    CustomInt1 = int.Parse(row["CUSTOMINT1"].ToString()),
                    CustomInt2 = int.Parse(row["CUSTOMINT2"].ToString()),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString()),
                });
            };
            
            return nameValueList;
        }
    }
}
