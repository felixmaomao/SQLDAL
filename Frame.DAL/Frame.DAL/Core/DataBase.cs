using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Frame.DAL.Core
{
    public class DataBase
    {
        #region Data
        private string _name;
        private string _connectionString;
        private List<StoredProcedure> _procedureList = new List<StoredProcedure>();
        #endregion

        #region Constructors
        public DataBase() { }
        public DataBase(string name,string connectionString)
        {
            this._name = name;
            this._connectionString = connectionString;
        }
        #endregion

        #region Methods
        

        public void AddProcedure(StoredProcedure procedure)
        {
            if (!_procedureList.Exists(p=>p.Code==procedure.Code))
            {
                _procedureList.Add(procedure);
            }
        }




        #endregion


    }
}
