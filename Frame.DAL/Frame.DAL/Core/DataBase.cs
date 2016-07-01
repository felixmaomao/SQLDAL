using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Xml.Linq;
using System.Configuration;
using System.Data.SqlClient;

namespace Frame.DAL.Core
{
    public class DataBase
    {
        #region Data
        private string _name;
        private string _connectionString = string.Empty;
        private string _filePath = string.Empty;
        private List<StoredProcedure> _procedureList = new List<StoredProcedure>();
        #endregion

        #region Constructors
        public DataBase() {
            EnsureProceListInitialized();
        }
        public DataBase(string name,string connectionString)
        {
            this._name = name;
            this._connectionString = connectionString;
            EnsureProceListInitialized();
        }
        #endregion

        #region Methods

        public void EnsureProceListInitialized()
        {
            string xmlpath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["FilePathData"].ToString();
            XDocument doc = XDocument.Load(xmlpath);
            string connstr = doc.Root.Element("database").Attribute("connectionString").Value;
            XElement root = doc.Root;
            //存储过程节点
            IEnumerable<XElement> suitableElements = root.Element("database").Elements();
            //是否需要缓存优化？
            foreach (XElement proceItem in suitableElements.Elements())
            {
                IEnumerable<Parameter> sqlParameters = from item in proceItem.Elements()
                                                       select new Parameter(item.Attribute("name").Value, item.Attribute("type").Value, Convert.ToInt32(item.Attribute("size").Value), Convert.ToInt32(item.Attribute("direction").Value));
                SqlParameter[] parameters = new SqlParameter[sqlParameters.Count()];
                List<Parameter> sqlParametersList = sqlParameters.ToList();
                for (int i = 0; i < parameters.Count(); i++)
                {
                    Parameter param = sqlParametersList[i];
                    SqlParameter sqlParam = new SqlParameter(param.Name, param.Type, param.Size);
                    sqlParam.Direction = param.Direction;
                    parameters[i] = sqlParam;
                }
                StoredProcedure procedure = new StoredProcedure { SqlParameters = parameters };
                _procedureList.Add(procedure);
            }                               
        }


        public void AddProcedure(StoredProcedure procedure)
        {
            if (!_procedureList.Exists(p=>p.Name==procedure.Name))
            {
                _procedureList.Add(procedure);
            }
        }

        public XElement ExecuteRtnXml(string procedureName,ref string rtnXml,params object[] paramsValue)
        {
            //find the procedure 
            //prepare the procedure
            //execute the procedure
            StoredProcedure procedure = FindProcedureByName(procedureName);
            procedure.Execute();           
        }

        public StoredProcedure FindProcedureByName(string procedureName)
        {
            return _procedureList.First(p => p.Name == procedureName);
        }
                        
        #endregion


    }
}
