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
        private static DataBase _dbMainSlave;
        private static DataBase _dbFrame ;
        private static Dictionary<int, DataBase> _dict = new Dictionary<int, DataBase>();

        static Procedure()
        {
            _dbMain  = new DataBase("FilePathData");
            _dbMainSlave = new DataBase("FilePathData", MasterSlave.Slave);
            _dbFrame = new DataBase("FilePathFrame");

            _dict.Add(0,_dbMain);
            _dict.Add(1,_dbFrame);
            _dict.Add(2,_dbMainSlave);
        }

        #region Methods        
        //结果集
        public static XElement ExecuteToXml(int dbNo,string procedureName,params object[] paramsValue)
        {
            DataBase db;
            if (_dict.TryGetValue(dbNo,out db))
            {
                return db.Execute(procedureName,paramsValue);
            }
            return null;
        }
        //结果集，参数列表
        public static XElement ExecuteToXml(int dbNo,string procedureName,ref string rtnXml,params object[] paramsValue)
        {
            DataBase db;            
            if(_dict.TryGetValue(dbNo,out db))
            {
                return db.Execute(procedureName,ref rtnXml,paramsValue);
            }
            return null;
        }
        //对于所有没有查询结果集的存储过程，均用此方法 获得返回参数xelement.
        public static XElement ExecuteNonQuery(int dbNo, string procedureName, params object[] paramsValue)
        {
            DataBase db;
            if (_dict.TryGetValue(dbNo, out db))
            {
                XElement outXml = null;
                db.ExecuteNonQuery(procedureName,ref outXml,paramsValue);
                return outXml;
            }
            return null;
        }
        #endregion

        #region CommonMehtods

        //沈伟 将xml字符串转化为xml对象
        public static XElement ConvertToXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            return XElement.Parse(xml);
        }

        #endregion
    }
}
