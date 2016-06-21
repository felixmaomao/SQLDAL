using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Frame.DAL.Core
{
    public class StoredProcedure
    {
        private string _name;
        private string _code;
        private string _connectionString;
        private SqlParameter[] _sqlParameters;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public StoredProcedure(string name,string code,string connectionString)
        {
            this._name = name;
            this._code = code;
            this._connectionString = connectionString;
        }

        public DataTable ExecuteToDataTable()
        {
            return SQLHelper.ExecuteToDataTable(_connectionString,_code,_sqlParameters);
        }

        public DataTable ExecuteToDataTable(ref string rtnxml)
        {
            return SQLHelper.ExecuteToDataTable(_connectionString,_code,_sqlParameters,ref rtnxml);
        }
    }
}
