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
        public DataBase(string filePathName) {
            this._filePath= AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings[filePathName].ToString();
            EnsureProceListInitialized();
        }
       
        #endregion

        #region Methods

        public void EnsureProceListInitialized()
        {            
            XDocument doc = XDocument.Load(_filePath);
            string connstr = doc.Root.Element("database").Attribute("connectionString").Value;
            this._connectionString = connstr;
            XElement root = doc.Root;
            //存储过程节点
            IEnumerable<XElement> suitableElements = root.Element("database").Elements();
            //是否需要缓存优化？
            foreach (XElement proceItem in suitableElements)
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
                StoredProcedure procedure = new StoredProcedure { SqlParameters = parameters, Name = proceItem.Attribute("name").Value };
                AddProcedure(procedure);
            }                               
        }

        public void AddProcedure(StoredProcedure procedure)
        {
            if (!_procedureList.Exists(p=>p.Name==procedure.Name))
            {
                _procedureList.Add(procedure);
            }
        }

        public int ExecuteRtnXml(string procedureName,ref string rtnXml,params object[] paramsValue)
        {          
            StoredProcedure procedure = FindProcedureByName(procedureName);
            FillInTheProcedureWithValues(procedure,paramsValue);
            int result=SQLHelper.ExecuteNonQuery(this._connectionString,procedureName,procedure.SqlParameters,ref rtnXml);
            return result;
        }

        public string Execute(string procedureName, ref string rtnXml,params object[] paramsValue)
        {
            StoredProcedure procedure = FindProcedureByName(procedureName);
            FillInTheProcedureWithValues(procedure,paramsValue);
            return string.Empty;
        }

        public StoredProcedure FindProcedureByName(string procedureName)
        {
            return _procedureList.First(p => p.Name == procedureName);
        }

        public void FillInTheProcedureWithValues(StoredProcedure procedure,params object[] paramsValue)
        {
            //按顺序填充存储过程所需参数值
            if (paramsValue.Length>0)
            {
                for (int i = 0; i < procedure.SqlParameters.Count(); i++)
                {
                    procedure.SqlParameters[i].Value = paramsValue[i];
                }
            }            
       }
                        
        #endregion

    }
}
