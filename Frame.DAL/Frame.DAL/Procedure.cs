using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frame.DAL.Core;
using System.Data;
using System.Xml.Linq;
namespace Frame.DAL
{
    public class Procedure
    {
        private static DataBase _dbMain ;
        private static DataBase _dbFrame ;
        private static Dictionary<int, DataBase> _dict = new Dictionary<int, DataBase>();

        static Procedure()
        {
            _dbMain  = new DataBase("FilePathData");
            _dbFrame = new DataBase("FilePathFrame");

            _dict.Add(1,_dbMain);
            _dict.Add(2,_dbFrame);
        }
                    
        public static DataTable SelectToDataTable(int dbNo,params object[] paramValues)
        {
            DataBase db = _dict[dbNo];
            if(db!=null)
            {
                                
            }
            return null;
        }

        public static XElement ExecuteToXml(int dbNo,params object[] paramValues)
        {
            DataBase db = _dict[dbNo];
            if (db!=null)
            {

            }
            return null;
        }

        public static string ExecuteToString(int dbNo,string procedureName,params object[] paramsValues)
        {
            DataBase db;
            string rtnxml = string.Empty;
            if(_dict.TryGetValue(dbNo,out db))
            {
                return db.Execute(procedureName,ref rtnxml,paramsValues);
            }
            return null;
        }

    }
}
