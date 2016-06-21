using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Frame.DAL.Core
{
    public class SQLHelper
    {

        public static int ExecuteNonQuery(string connectionString,string cmdText,SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                InitCommand(cmd,connection,cmdText,parameters);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        public static int ExecuteNonQuery(string connectionString, string cmdText, SqlParameter[] parameters,ref string rtnxml)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                InitCommand(cmd, connection, cmdText, parameters);
                int result = cmd.ExecuteNonQuery();
                rtnxml=ConvertOutputToXml(cmd.Parameters);
                return result;
            }
        }

        public static DataTable ExecuteToDataTable(string connectionString,string cmdText,SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {                
                InitCommand(cmd,connection,cmdText,parameters);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable table = new DataTable();
                table.Load(dataReader);
                dataReader.Close();
                return table;
            }
        }

        public static DataTable ExecuteToDataTable(string connectionString, string cmdText, SqlParameter[] parameters, ref string rtnxml)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                InitCommand(cmd,connection,cmdText,parameters);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable table = new DataTable();
                table.Load(dataReader);
                dataReader.Close();
                rtnxml=ConvertOutputToXml(cmd.Parameters);
                return table;
            }
        }

        public static void InitCommand(SqlCommand cmd,SqlConnection connection,string cmdText,SqlParameter[] parameters)
        {
            cmd.Connection = connection;
            cmd.CommandText = cmdText;
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
        }

        public static string ConvertOutputToXml(SqlParameterCollection cmdParams)
        {
            return null;
        }

    }
}
