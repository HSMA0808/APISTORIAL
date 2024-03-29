﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class Record : EntityBase
    {
        public Patient patient { get; set; }
        public MedicalCenter medicalCenter_Creator { get; set; }
        public MedicalCenter last_MedicalCenterUpdate { get; set; }

        public Record()
        {
            patient = new Patient();
            medicalCenter_Creator = new MedicalCenter();
            last_MedicalCenterUpdate = new MedicalCenter();
        }
        public int Save(string connectionString)
        {
            var db = new DBConnection(connectionString);
            var cmd = new SqlCommand();
            cmd.CommandText = @"EXEC INSERT_RECORD @IDPATIENT, @MEDICALCENTER_CREATOR, @LAST_MEDICALCENTER_UPDATE, @CREATE_USER, @CREATE_DATE)";

            cmd.Parameters.Add(new SqlParameter("@IDPATIENT", patient.ID));
            cmd.Parameters.Add(new SqlParameter("@MEDICALCENTER_CREATOR", medicalCenter_Creator.ID));
            cmd.Parameters.Add(new SqlParameter("@LAST_MEDICALCENTER_UPDATE", last_MedicalCenterUpdate.ID));
            cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            cmd.Parameters.Add(new SqlParameter("@CREATE_USER", CreateUser));
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("SELECT TOP(1) * FROM RECORD ORDER BY CREATE_DATE DESC"));
            return int.Parse(ds.Tables[0].Rows[0]["IDRECORD"].ToString());
        }

        public List<Record> ToList(string connectionString, int Top = 0)
        {
            var db = new DBConnection(connectionString);
            var medicalCenter = new MedicalCenter(); 
            var cmd = new SqlCommand();
            var recordList = new List<Record>();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP("+Top+") * FROM RECORD";
                cmd.Parameters.Add(new SqlParameter("@Top", Top));
            }
            else
            {
                cmd.CommandText = @"SELECT * FROM RECORD ";

            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                recordList.Add(new Record() { 
                    ID = Convert.ToInt32(row["ID"].ToString()),
                    last_MedicalCenterUpdate = medicalCenter.Find(int.Parse(row["IDMEDICAL_CENTER"].ToString()), connectionString),
                    CreateDate = DateTime.Parse(row["CREATE_DATE"].ToString()),
                    CreateUser = row["CREATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(row["UPDATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString()
                });
            }
            return recordList;
        }

        public Record Find(int id, string connectionString)
        {
            var db = new DBConnection(connectionString);
            var oPatient = new Patient();
            var medicalCenter = new MedicalCenter();
            var cmd = new SqlCommand();
            var recordList = new List<Record>();
            cmd.CommandText = @"SELECT * FROM RECORD
                              WHERE IDRECORD = " + id + "";
            var ds = db.ExtractDataSet(cmd);
            return new Record()
            {
                ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"].ToString()),
                patient = oPatient.Find(int.Parse(ds.Tables[0].Rows[0]["IDPATIENT"].ToString()), connectionString),
                medicalCenter_Creator = medicalCenter.Find(int.Parse(ds.Tables[0].Rows[0]["MEDICALCENTER_CREATOR"].ToString()), connectionString),
                last_MedicalCenterUpdate = medicalCenter.Find(int.Parse(ds.Tables[0].Rows[0]["LAST_MEDICALCENTER_UPDATE"].ToString()), connectionString),
                CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString()),
                UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString()
            };
        }
    }

}
