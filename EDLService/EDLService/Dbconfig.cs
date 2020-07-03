using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EDLService
{
    public class Dbconfig
    {
        private static readonly string ConnectStr = "Data Source=JDB;User Id=JDB;Password = 123456;";

        public static OracleConnection GetConnection()
        {
            try
            {
                return new OracleConnection(ConnectStr);
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex.GetBaseException();
            }
        }

        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                GetConnection().Open();
                OracleDataAdapter da = new OracleDataAdapter(sql,GetConnection());
                da.Fill(dt);
                DisConnect();
            }
            catch (OracleException ex)
            {
                dt = new DataTable();
                dt.Columns.Add("ERROR_CODE", typeof(string));
                dt.Columns.Add("ERROR_DESC", typeof(string));
                dt.Rows.Add("99", ex.Message);
            }

            return dt;
        }
        private void DisConnect()
        {
            GetConnection().Close();
            GetConnection().Dispose();
        }

        public static OracleDataReader ExecuteReader(string Stmt)
        {
            OracleConnection Con = GetConnection();
            try
            {
                Con.Open();
                Con.CreateCommand();
                return new OracleCommand(Stmt, Con).ExecuteReader();
            }
            catch (OracleException ex)
            {
                Con.Close();
                Console.WriteLine(ex.Message);
                throw ex.GetBaseException();
            }
        }

        public static int ExecuteNonQuery(string Stmt)
        {
            OracleConnection Con = GetConnection();
            try
            {
                Con.Open();
                Con.BeginTransaction().Commit();
                return new OracleCommand(Stmt, Con).ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Con.BeginTransaction().Rollback();
                Con.Close();
                Console.WriteLine(ex.Message);
                throw ex.GetBaseException();
            }
        }

        public static object ExecuteScalar(string Stmt,OracleCommand cmd)
        {
            OracleConnection Con = GetConnection();
            try
            {
                Con.Open();
                Con.BeginTransaction().Commit();
                cmd.CommandText = Stmt;
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd.ExecuteScalar();
            }
            catch (OracleException ex)
            {
                Con.BeginTransaction().Rollback();
                Con.Close();
                Console.WriteLine(ex.Message);
                throw ex.GetBaseException();
            }
        }
    }
}