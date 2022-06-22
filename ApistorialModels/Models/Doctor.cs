using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ApistorialModels.Models
{
    public class Doctor : EntityBase
    {
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Sex { get; set; }
        public NameValue NvIdentification_Type { get; set; }
        public string Identfication_Number { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public Specialtys specialty { get; set; }
        public NameValue NvBlood { get; set; }
        public string Tel1 { get; set; }

        public string Tel2 { get; set; }
        public string Email { get; set; }

        public Doctor()
        {
            NvIdentification_Type = new NameValue();
            specialty = new Specialtys();
            NvBlood = new NameValue();
        }
        public int Save(string connectionString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"EXEC INSERT_DOCTOR FIRST_NAME, @MIDDLE_NAME, @LAST_NAME, @SEX, @NVIDENTIFICATION_TYPE
                                , @IDENTIFICATION_NUMBER, @ADDRESS1, @ADDRESS2, @IDSPECIALTY, @NVBLOOD_TYPE, @TEL1, @TEL2, @EMAIL
                                , @CREATE_USER, @CREATE_DATE)";
            cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", First_Name));
            cmd.Parameters.Add(new SqlParameter("@MIDDLE_NAME", Middle_Name));
            cmd.Parameters.Add(new SqlParameter("@LAST_NAME", Last_Name));
            cmd.Parameters.Add(new SqlParameter("@SEX", Sex));
            cmd.Parameters.Add(new SqlParameter("@NVIDENTIFICATION_TYPE", NvIdentification_Type.ID));
            cmd.Parameters.Add(new SqlParameter("@IDENTIFICATION_NUMBER", Identfication_Number));
            cmd.Parameters.Add(new SqlParameter("@ADDRESS1", Address1));
            cmd.Parameters.Add(new SqlParameter("@ADDRESS2", Address2));
            cmd.Parameters.Add(new SqlParameter("@IDSPECIALTY", specialty.ID));
            cmd.Parameters.Add(new SqlParameter("@NVBLOOD_TYPE", NvBlood.ID));
            cmd.Parameters.Add(new SqlParameter("@TEL1", Tel1));
            cmd.Parameters.Add(new SqlParameter("@TEL2", Tel2));
            cmd.Parameters.Add(new SqlParameter("@EMAIL", Email));
            cmd.Parameters.Add(new SqlParameter("@CREATE_USER", CreateUser));
            cmd.Parameters.Add(new SqlParameter("@CREATE_DATE", CreateDate));
            var db = new DBConnection(connectionString);
            db.ExecuteCommand(cmd);
            var ds = db.ExtractDataSet(new SqlCommand("select top(1) * from DOCTOR"));
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        public void Update(string connectionString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE DOCTOR SET 
                                  FIRST_NAME = @FIRST_NAME, 
                                  MIDDLE_NAME = @MIDDLE_NAME, 
                                  LAST_NAME = @LAST_NAME, 
                                  SEX = @SEX, 
                                  NVIDENTIFICATION_TYPE = @NVIDENTIFICATION_TYPE
                                , IDENTIFICATION_NUMBER = @IDENTIFICATION_NUMBER, 
                                  ADDRESS1 = @ADDRESS1, 
                                  ADDRESS2 = @ADDRESS2, 
                                  IDSPECIALTY = @IDSPECIALTY, NVBLOOD_TYPE = @NVBLOOD_TYPE
                                , TEL1 = @TEL1, 
                                  TEL2 = @TEL2, 
                                  EMAIL = @EMAIL, 
                                  UPDATE_USER = @UPDATE_USER, 
                                  UPDATE_DATE = @UPDATE_DATE
                                WHERE 
                                  IDDOCTOR = @IDDOCTOR";

            cmd.Parameters.Add(new SqlParameter("@IDDOCTOR", ID));
            cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", First_Name));
            cmd.Parameters.Add(new SqlParameter("@MIDDLE_NAME", Middle_Name));
            cmd.Parameters.Add(new SqlParameter("@LAST_NAME", Last_Name));
            cmd.Parameters.Add(new SqlParameter("@SEX", Sex));
            cmd.Parameters.Add(new SqlParameter("@NVIDENTIFICATION_TYPE", NvIdentification_Type.ID));
            cmd.Parameters.Add(new SqlParameter("@IDENTIFICATION_NUMBER", Identfication_Number));
            cmd.Parameters.Add(new SqlParameter("@ADDRESS1", Address1));
            cmd.Parameters.Add(new SqlParameter("@ADDRESS2", Address2));
            cmd.Parameters.Add(new SqlParameter("@IDSPECIALTY", specialty.ID));
            cmd.Parameters.Add(new SqlParameter("@NVBLOOD_TYPE", NvBlood.ID));
            cmd.Parameters.Add(new SqlParameter("@TEL1", Tel1));
            cmd.Parameters.Add(new SqlParameter("@TEL2", Tel2));
            cmd.Parameters.Add(new SqlParameter("@EMAIL", Email));
            cmd.Parameters.Add(new SqlParameter("@UPDATE_USER", UpdateUser));
            cmd.Parameters.Add(new SqlParameter("@UPDATE_DATE", UpdateDate));
            var db = new DBConnection(connectionString);
            db.ExecuteCommand(cmd);
        }

        public List<Doctor> ToList(string connectionString, int Top = 0)
        {
            var DoctorList = new List<Doctor>();
            var db = new DBConnection(connectionString);
            var nameValue = new NameValue();
            var cmd = new SqlCommand();
            if (Top > 0)
            {
                cmd.CommandText = @"SELECT TOP("+Top+") * FROM DOCTOR";
            }
            else
            {
                cmd.CommandText = @"SELECT * FROM DOCTOR";
            }
            var ds = db.ExtractDataSet(cmd);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                DoctorList.Add(new Doctor()
                {
                    ID = int.Parse(row["ID"].ToString()),
                    First_Name = row["FIRST_NAME"].ToString(),
                    Middle_Name = row["MIDDLE_NAME"].ToString(),
                    Last_Name = row["LAST_NAME"].ToString(),
                    Sex = row["SEX"].ToString(),
                    NvIdentification_Type = nameValue.Find(int.Parse(row["NVIDENTIFICATION_TYPE"].ToString()), connectionString),
                    Identfication_Number = row["IDENTIFICATION_NUMBER"].ToString(),
                    Address1 = row["ADDRESS1"].ToString(),
                    Address2 = row["ADDRESS2"].ToString(),
                    specialty = new Specialtys() { ID = int.Parse(row["IDSPECIALTY"].ToString())},
                    NvBlood = nameValue.Find(int.Parse(row["NVBLOOD_TYPE"].ToString()), connectionString),
                    Tel1 = row["TEL1"].ToString(),
                    Tel2 = row["TEL2"].ToString(),
                    Email = row["EMAIL"].ToString(),
                    CreateUser = row["CREATE_USER"].ToString(),
                    CreateDate =  DateTime.Parse(row["CREATE_DATE"].ToString()),
                    UpdateUser = row["UPDATE_USER"].ToString(),
                    UpdateDate =  DateTime.Parse(row["UPDATE_DATE"].ToString()),
                });
            }
            return DoctorList;
        }

        public Doctor Find(int idDoctor, string connectionString)
        {
            var DoctorList = new List<Doctor>();
            var db = new DBConnection(connectionString);
            var oNameValue = new NameValue();
            var oSpecialty = new Specialtys();
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM DOCTOR WHERE IDDOCTOR = "+idDoctor+"";
            var ds = db.ExtractDataSet(cmd);
            return new Doctor()
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString()),
                    First_Name = ds.Tables[0].Rows[0]["FIRST_NAME"].ToString(),
                    Middle_Name = ds.Tables[0].Rows[0]["MIDDLE_NAME"].ToString(),
                    Last_Name = ds.Tables[0].Rows[0]["LAST_NAME"].ToString(),
                    Sex = ds.Tables[0].Rows[0]["SEX"].ToString(),
                    NvIdentification_Type = oNameValue.Find(int.Parse(ds.Tables[0].Rows[0]["NVIDENTIFICATION_TYPE"].ToString()), connectionString),
                    Identfication_Number = ds.Tables[0].Rows[0]["IDENTIFICATION_NUMBER"].ToString(),
                    Address1 = ds.Tables[0].Rows[0]["ADDRESS1"].ToString(),
                    Address2 = ds.Tables[0].Rows[0]["ADDRESS2"].ToString(),
                    specialty = oSpecialty.Find(int.Parse(ds.Tables[0].Rows[0]["IDSPECIALTY"].ToString()), connectionString),
                    NvBlood = oNameValue.Find(int.Parse(ds.Tables[0].Rows[0]["NVBLOOD_TYPE"].ToString()), connectionString),
                    Tel1 = ds.Tables[0].Rows[0]["TEL1"].ToString(),
                    Tel2 = ds.Tables[0].Rows[0]["TEL2"].ToString(),
                    Email = ds.Tables[0].Rows[0]["EMAIL"].ToString(),
                    CreateUser = ds.Tables[0].Rows[0]["CREATE_USER"].ToString(),
                    CreateDate = DateTime.Parse(ds.Tables[0].Rows[0]["CREATE_DATE"].ToString()),
                    UpdateUser = ds.Tables[0].Rows[0]["UPDATE_USER"].ToString(),
                    UpdateDate = DateTime.Parse(ds.Tables[0].Rows[0]["UPDATE_DATE"].ToString()),
                };
            }
        }
    }


