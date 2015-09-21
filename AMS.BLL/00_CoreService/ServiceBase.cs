using System;
using AMS.DAL;

namespace AMS.BLL
{
    /// <summary>
    /// 服务基类
    /// 封装数据层：NHibernate、Castle AR、企业库等
    /// </summary>
    public abstract partial class ServiceBase : MarshalByRefObject
    {
        #region 数据库访问设置

        #region 工厂模式
        /// <summary>
        /// 工厂模式下的数据库访问接口
        /// </summary>
        public static IDbHelper DbHelper
        {
            get
            {
                return DataFactory.SqlHelper();
            }
        }
        /// <summary>
        /// 工厂模式下的数据库常用接口
        /// </summary>
        public static IDbUtilities DbUtils
        {
            get
            {
                return DataFactory.DbUtils();
            }
        }
        #endregion

        #region 微软企业库
        /// <summary>
        /// 微软企业库
        /// </summary>
        public static DataBaseFactory DBEntLib
        {
            get
            {
                return new DataBaseFactory(null, null);
            }
        }


        /// <summary>
        /// 微软企业库数据库访问助手
        /// </summary>
        public static DatabaseHelper DbEntHelper
        {
            get
            {
                return DBEntLib.DBHelper;
            }
        }

        #endregion

        #endregion
    }
}
