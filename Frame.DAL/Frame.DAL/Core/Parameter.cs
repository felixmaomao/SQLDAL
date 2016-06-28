using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Frame.DAL.Core
{
    public class Parameter
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private SqlDbType _type;
        public SqlDbType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private int _size;
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
        private ParameterDirection _direction;
        public ParameterDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        #region constructors
        public Parameter() { }

        public Parameter(string paramName,string paramType,int paramSize,int direction)
        {
            _name = paramName;
            _type = SqlTypeStringToSqlDBType(paramType);
            _size = paramSize;
            _direction = (ParameterDirection)direction;            
        }
        #endregion

        public SqlDbType SqlTypeStringToSqlDBType(string sqlTypeString)
        {
            SqlDbType dbType = SqlDbType.Variant;
            sqlTypeString = sqlTypeString.Trim().ToLower();
            switch (sqlTypeString)
            {
                case "int":
                    dbType = SqlDbType.Int;
                    break;
                case "varchar":
                    dbType = SqlDbType.VarChar;
                    break;
                case "bit":
                    dbType = SqlDbType.Bit;
                    break;
                case "datetime":
                    dbType = SqlDbType.DateTime;
                    break;
                case "decimal":
                    dbType = SqlDbType.Decimal;
                    break;
                case "float":
                    dbType = SqlDbType.Float;
                    break;
                case "image":
                    dbType = SqlDbType.Image;
                    break;
                case "money":
                    dbType = SqlDbType.Money;
                    break;
                case "ntext":
                    dbType = SqlDbType.NText;
                    break;
                case "nvarchar":
                    dbType = SqlDbType.NVarChar;
                    break;
                case "smalldatetime":
                    dbType = SqlDbType.SmallDateTime;
                    break;
                case "smallint":
                    dbType = SqlDbType.SmallInt;
                    break;
                case "text":
                    dbType = SqlDbType.Text;
                    break;
                case "bigint":
                    dbType = SqlDbType.BigInt;
                    break;
                case "binary":
                    dbType = SqlDbType.Binary;
                    break;
                case "char":
                    dbType = SqlDbType.Char;
                    break;
                case "nchar":
                    dbType = SqlDbType.NChar;
                    break;
                case "numeric":
                    dbType = SqlDbType.Decimal;
                    break;
                case "real":
                    dbType = SqlDbType.Real;
                    break;
                case "smallmoney":
                    dbType = SqlDbType.SmallMoney;
                    break;
                case "sql_variant":
                    dbType = SqlDbType.Variant;
                    break;
                case "timestamp":
                    dbType = SqlDbType.Timestamp;
                    break;
                case "tinyint":
                    dbType = SqlDbType.TinyInt;
                    break;
                case "uniqueidentifier":
                    dbType = SqlDbType.UniqueIdentifier;
                    break;
                case "varbinary":
                    dbType = SqlDbType.VarBinary;
                    break;
                case "xml":
                    dbType = SqlDbType.Xml;
                    break;
            }
            return dbType;
        
        }

    }
}
