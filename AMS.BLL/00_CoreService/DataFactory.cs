using AMS.DAL;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 数据库服务工厂
    /// </summary>
    public class DataFactory
    {
        /// <summary>
        /// 当前数据库类型
        /// </summary>
        public static SqlSourceType DbType 
        {
            get
            {
                string strDbType = ConfigHelper.GetValue("ComponentDbType");
                if (strDbType == "Oracle")
                {
                    return SqlSourceType.Oracle;
                }
                else if (strDbType == "SQLServer")
                {
                    return SqlSourceType.SQLServer;
                }
                else if (strDbType == "MySql")
                {
                    return SqlSourceType.MySql;
                }
                else if (strDbType == "SqLite")
                {
                    return SqlSourceType.SqLite;
                }
                else if (strDbType == "Access")
                {
                    return SqlSourceType.Access;
                }
                else
                {
                    return SqlSourceType.Oracle;
                }
            }
        }

        /// <summary>
        /// 获取指定的数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbHelper SqlHelper()
        {
            switch (DbType)
            {
                case SqlSourceType.SQLServer:
                    return SqlHelper(ConfigHelper.GetDbConnectValue("DefaultConnectionString"));
                case SqlSourceType.Oracle:
                    return SqlHelper(ConfigHelper.GetDbConnectValue("DefaultConnectionString"));
                case SqlSourceType.MySql:
                    return SqlHelper(ConfigHelper.GetDbConnectValue("DefaultConnectionString"));
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 获取指定的数据库连接
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IDbHelper SqlHelper(string connectionString)
        {
            switch (DbType)
            {
                case SqlSourceType.SQLServer:
                    return new SqlServerHelper(connectionString);
                case SqlSourceType.Oracle:
                    return new OracleHelper(connectionString);
                case SqlSourceType.MySql:
                    return new MySqlHelper(connectionString);
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 公共方法操作 增、删、改、查
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public static IDbUtilities DbUtils(string connectionString = null)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                return new DbUtilities(connectionString, DbType);
            }
            else
            {
                switch (DbType)
                {
                    case SqlSourceType.Oracle:
                        return new DbUtilities(ConfigHelper.GetDbConnectValue("DefaultConnectionString"), DbType);
                    case SqlSourceType.SQLServer:
                        return new DbUtilities(ConfigHelper.GetDbConnectValue("DefaultConnectionString"), DbType);
                    case SqlSourceType.MySql:
                        return new DbUtilities(ConfigHelper.GetDbConnectValue("DefaultConnectionString"), DbType);
                    default:
                        break;
                }
            }
            return null;
        }
    }
}
