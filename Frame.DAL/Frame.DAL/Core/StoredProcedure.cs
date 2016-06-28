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
        private SqlParameter[] _sqlParameters;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public StoredProcedure(string name)
        {
            this._name = name;    
        }
     
    }
}
