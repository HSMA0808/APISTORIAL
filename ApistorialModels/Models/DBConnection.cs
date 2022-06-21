using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ApistorialModels.Models
{
    internal class DBConnection
    {
        public SqlConnection conex { get; set; }
        public SqlConnection cmd { get; set; }
        public SqlDataAdapter adapter { get; set; }
        public string connectionString { get; set; }

        public DBConnection(string conString)
        {
            connectionString = conString;
        }

        public SqlConnection Conectar()
        {
            try
            {
                conex = new SqlConnection(connectionString);
                conex.Open();
            }
            catch (Exception e)
            {
                conex.Close();
                throw new Exception("Ha ocurrido un error tratando de conectar con la base de datos: " + e);
            }
            return conex;
        }

        public bool ExecuteCommand(SqlCommand cmd)
        {
            bool Ok = false;
            var conexion = Conectar();
            try
            {
                cmd.Connection = conexion;
                cmd.Transaction = conexion.BeginTransaction();
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
                Ok = true;
                conexion.Close();
            }
            catch (Exception e)
            {
                cmd.Transaction.Rollback();
                conexion.Close();
                throw new Exception("Ha ocurrido un error ejecutando un comando : " + e);
            }
            return Ok;
        }

        public DataSet ExtractDataSet(SqlCommand cmd)
        {
            var conexion = Conectar();
            var dataSet = new DataSet();
            try
            {
                adapter = new SqlDataAdapter(cmd.CommandText, conexion);
                adapter.Fill(dataSet);
                conexion.Close();
            }
            catch (Exception e)
            {
                conexion.Close();
                throw new Exception("Ha ocurrido un error realizando la consulta: " + e);
            }
            return dataSet;
        }
    }
}
