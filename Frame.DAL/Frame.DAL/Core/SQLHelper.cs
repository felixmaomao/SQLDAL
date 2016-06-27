using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
namespace Frame.DAL.Core
{
    public class SQLHelper
    {

        public static int ExecuteNonQuery(string connectionString,string cmdText,SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
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
                connection.Open();
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
                connection.Open();
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
                connection.Open();
                InitCommand(cmd,connection,cmdText,parameters);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable table = new DataTable();
                table.Load(dataReader);
                dataReader.Close();
                rtnxml=ConvertOutputToXml(cmd.Parameters);
                return table;
            }
        }
        
        //沈伟 
        public static SqlDataReader ExecuteReader(string connectionString, string cmdText, SqlParameter[] parameters, ref string rtnxml)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                InitCommand(cmd,connection,cmdText,parameters);
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                dataReader.Close();
                return dataReader;
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

        //沈伟 将sqldatareader转化成xml字符串
        public static string ConvertReaderToXml(SqlDataReader reader)
        {
            string Pattern = @"[\<\>&]";
            Regex regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("read");
            do
            {
                while (reader.Read())
                {
                    XmlElement item = doc.CreateElement("item");
                    for (int ii = 0; ii < reader.FieldCount; ii++)
                    {
                        XmlElement node = doc.CreateElement(reader.GetName(ii));
                        string value = reader.GetValue(ii).ToString();
                        if (regex.IsMatch(value))
                        {
                            node.AppendChild(doc.CreateCDataSection(value));
                        }
                        else
                        {
                            node.InnerText = value;
                        }
                        item.AppendChild(node);
                    }
                    root.AppendChild(item);
                }
            }
            while (reader.NextResult());
            doc.AppendChild(root);
            return doc.OuterXml;
        }

        //沈伟 将xml字符串转化为xml对象
        public static XElement ConvertToXml(string xml)
        {
            if (!string.IsNullOrEmpty(xml))
            {
                return null;
            }
            return XElement.Parse(xml);
        }

    }
}
