﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class MedicalCenter : EntityBase
    {
        public string Description { get; set; }
        public string RNC { get; set; }
        public string Tel1 { get; set; }
        public string  Tel2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string  Name_Contact { get; set; }
        public NameValue NVStatus_Center { get; set; }
        public string Token { get; set; }

        public int Save(string connectionString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO Analysis 
                                (DESCRIPTION, RNC, TEL1, TEL2, EMAIL1, EMAIL2
                                , NVSTATUS_CENTER, TOKEN, CREATE_USER, CREATE_DATE) 
                                VALUES 
                                (@DESCRIPTION, @RNC, @TEL1, @TEL2, @EMAIL1, @EMAIL2
                                , @NVSTATUS_CENTER, @TOKEN, @CREATE_USER, @CREATE_DATE)";

            cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", Description));
            cmd.Parameters.Add(new SqlParameter("@RNC", RNC));
            cmd.Parameters.Add(new SqlParameter("@TEL1", Tel1));
            cmd.Parameters.Add(new SqlParameter("@TEL2", Tel2));
            cmd.Parameters.Add(new SqlParameter("@EMAIL1", Email1));
            cmd.Parameters.Add(new SqlParameter("@EMAIL2", Email2));
            cmd.Parameters.Add(new SqlParameter("@NVESTATUS_CENTER", NVStatus_Center.ID));
            cmd.Parameters.Add(new SqlParameter("@TOKEN", Token));
            var db = new DBConnection(connectionString);
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("select top(1) * from MEDICAL_CENTER"));
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public void Update(string connectionString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE MEDICAL_CENTER SET 
                                                      DESCRIPTION = @DESCRIPTION, 
                                                      RNC = @RNC, 
                                                      TEL1 = @TEL1, 
                                                      TEL2 = @TEL2, 
                                                      EMAIL1 = @EMAIL1, 
                                                      EMAIL2 = @EMAIL2,
                                                      NVSTATUS_CENTER = @NVSTATUS_CENTER, 
                                                      TOKEN = @TOKEN, 
                                                      CREATE_USER = @CREATE_USER, 
                                                      CREATE_DATE = @CREATE_DATE) 
                                WHERE IDMEDICAL_CENTER = @IDMEDICAL_CENTER";

            cmd.Parameters.Add(new SqlParameter("@IDMEDICAL_CENTER", ID));
            cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", Description));
            cmd.Parameters.Add(new SqlParameter("@RNC", RNC));
            cmd.Parameters.Add(new SqlParameter("@TEL1", Tel1));
            cmd.Parameters.Add(new SqlParameter("@TEL2", Tel2));
            cmd.Parameters.Add(new SqlParameter("@EMAIL1", Email1));
            cmd.Parameters.Add(new SqlParameter("@EMAIL2", Email2));
            cmd.Parameters.Add(new SqlParameter("@NVESTATUS_CENTER", NVStatus_Center.ID));
            cmd.Parameters.Add(new SqlParameter("@TOKEN", Token));
            var db = new DBConnection(connectionString);
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("select top(1) * from Analysis"));
        }

        public List<MedicalCenter> ToList(string connectionString, int Top = 0)
        {
            var MedicalCenterList = new List<MedicalCenter>();
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP(@Top) * FROM MEDICAL_CENTER";
            }
            else
            {
                cmd.CommandText = @"SELECT * FROM MEDICAL_CENTER";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                MedicalCenterList.Add(new MedicalCenter()
                {
                    ID = int.Parse(row["IDMEDICAL_CENTER"].ToString()),
                    Description = row["DESCRIPTION"].ToString(),
                    RNC = row["RNC"].ToString(),
                    Tel1 = row["TEL1"].ToString(),
                    Tel2 = row["TEL2"].ToString(),
                    Email1 = row["EMAIL1"].ToString(),
                    Email2 = row["EMAIL2"].ToString(),
                    Name_Contact = row["Name_Contact"].ToString(),
                    NVStatus_Center = new NameValue() { ID = int.Parse(row["NVSTATUS_CENTER"].ToString())},
                    Token = row["TOKEN"].ToString(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString())

                });
            }
            return MedicalCenterList;
        }
    }
}
