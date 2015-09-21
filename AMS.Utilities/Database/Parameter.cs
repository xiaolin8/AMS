using System.Data;

namespace DotNet.Utilities
{
    public class Parameter
    {
        private string _Key;
        private object _Value;
        private ParameterDirection _Direction = ParameterDirection.Input;
        private DbType _DbType = DbType.String;

        public ParameterDirection Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        public DbType DbType
        {
            get { return _DbType; }
            set { _DbType = value; }
        }

        public Parameter(string sKey, object oValue, DbType oDbType = DbType.String)
        {
            this._Key = sKey;
            this._Value = oValue;
            this._DbType = oDbType;
        }

        public Parameter(string sKey, object oValue, ParameterDirection oDirection, DbType oDbType = DbType.String)
        {
            this._Key = sKey;
            this._Value = oValue;
            this._Direction = oDirection;
            this._DbType = oDbType;
        }
    }
}