using DotNet.Utilities;

namespace AMS.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class DataBaseFactory
    {

        DatabaseHelper dbHelper;

        private ClientCultureModel culture;
        private string _ConncetionString;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="connectionStringName"></param>
        public DataBaseFactory(ClientCultureModel culture, string connectionStringName = null)
        {
            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                connectionStringName = DefaultDataBaseHelper.GetDefaultDataBaseConnectStringName();
            }
            this._ConncetionString = connectionStringName;
            this.culture = culture;
        }


        /// <summary>
        /// ADO 
        /// </summary>
        public DatabaseHelper DBHelper
        {
            get
            {
                if (dbHelper == null)
                {
                    dbHelper = new DatabaseHelper(this._ConncetionString);

                }
                return dbHelper;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ClientCultureModel Culture
        {
            get { return culture; }
        }
    }
}
