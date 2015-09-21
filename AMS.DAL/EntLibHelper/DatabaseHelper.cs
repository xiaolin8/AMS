using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DotNet.Utilities;
using DotNet.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;

namespace AMS.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DatabaseHelper
    {

        private Database _db;
        private string _DBType;//Database Type 
 
        /// <summary>
        /// 
        /// </summary>
        public string DBType
        {
            get { return _DBType; }
        }
        private string _Key;//ConnectionString's Name  in config file
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DatabaseHelper(string key = "")
        {
            this._Key = key;
            CreateDatabase();

     
        }



        #region Scalar


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public object ExecuteScalar(string strString)
        {
            return ExecuteScalar(strString, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string strString, List<Parameter> parameters)
        {
            object oResult = null;

            if (_db == null) CreateDatabase();
            strString = PrepareSqlString(strString);
            if (parameters == null || parameters.Count == 0)
            {
                try
                {
                    oResult = _db.ExecuteScalar(CommandType.Text, strString);
                }
                catch (Exception ex)
                {
                    TraceLogHelper.Exception(strString, ex);
                }
            }
            else
            {
               
                DbCommand _cmd = _db.GetSqlStringCommand(strString);
                PrepareCommand(_cmd, parameters);
                try
                {
                    oResult = _db.ExecuteScalar(_cmd);
                }
                catch (Exception ex)
                {
                    TraceLogHelper.Exception(strString,parameters, ex);
                }
            }
            return oResult;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public object ExecuteScalarByStoredProcedure(string storedProcedureName, params object[] parameterValues)
        {
            object oResult = null;
            if (_db == null) CreateDatabase();
            oResult = _db.ExecuteScalar(storedProcedureName, parameterValues);
            return oResult;
        }
        #endregion

        #region  ExecuteNonQuery

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string strString)
        {
            return ExecuteNonQuery(strString, null);
        }
        /// <summary>
        /// 执行多条不带参数的SQL语句
        /// </summary>
        /// <param name="strStrings"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(List<string> strStrings)
        {
            int iResult = -1;
            if (_db == null) CreateDatabase();

            using (DbConnection connection = _db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (string strString in strStrings)
                    {
                        if (strString.Trim().Length > 1)
                        {
                            iResult = _db.ExecuteNonQuery(transaction, CommandType.Text, strString);
                        }
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();

                    iResult = -1;
                }
                finally
                {
                    connection.Close();
                }
            }
            return iResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string strString, List<Parameter> parameters)
        {
            int result = -1;
            if (_db == null) CreateDatabase();

            strString = PrepareSqlString(strString);
            if (parameters == null || parameters.Count == 0)
            {
              
                try
                {
                    result = _db.ExecuteNonQuery(CommandType.Text, strString);
                }
                catch(Exception ex)
                {
                    TraceLogHelper.Exception(strString,ex);
                }
            }
            else
            {
               
                DbCommand _cmd = _db.GetSqlStringCommand(strString);
                PrepareCommand(_cmd, parameters);
                try
                {
                    result = _db.ExecuteNonQuery(_cmd);
                }
                catch(Exception ex)
                {
                    TraceLogHelper.Exception(strString, parameters, ex);
                }
            }

            return result;
        }

        /// <summary>
        /// 执行多条带有参数的SQL语句
        /// </summary>
        /// <param name="_lstSqlAndPara"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(List<SqlTextAndParameter> _lstSqlAndPara)
        {

            int iResult = -1;
            if (_db == null) CreateDatabase();
            using (DbConnection connection = _db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                string strString = string.Empty;
                try
                {
                    foreach (SqlTextAndParameter _sqlTextAndPara in _lstSqlAndPara)
                    {
                        strString = _sqlTextAndPara.SqlString;
                        List<Parameter> oPara = _sqlTextAndPara.Parameters;
                        strString = PrepareSqlString(strString);
                        DbCommand _cmd = _db.GetSqlStringCommand(strString);
                        PrepareCommand(_cmd, oPara);
                        iResult = _db.ExecuteNonQuery(_cmd, transaction);
                    }


                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();

                    TraceLogHelper.Error(strString, ex);
                    iResult = -1;
                }
                finally
                {
                    connection.Close();
                }
            }
            return iResult;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public int ExecuteNonQueryByStoredProcedure(string storedProcedureName, params object[] parameterValues)
        {
            int oResult = -1;
            if (_db == null) CreateDatabase();
            try
            {
                oResult = _db.ExecuteNonQuery(storedProcedureName, parameterValues);
            }
            catch(Exception ex)
            {
                TraceLogHelper.Exception(storedProcedureName, parameterValues,ex);
            }
            return oResult;
        }
        #endregion
        #region DataReader

        /// <summary>
        /// 返回IDataReader
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public IDataReader ExecuteDataReader(string strString)
        {
            return ExecuteDataReader(strString, null);
        }

        /// <summary>
        /// 返回IDataReader
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDataReader ExecuteDataReader(string strString, List<Parameter> parameters)
        {
            IDataReader dtResult = null;

            if (_db == null) CreateDatabase();
            strString = PrepareSqlString(strString);
            if (parameters == null || parameters.Count == 0)
            {
                try
                {
                    dtResult = _db.ExecuteReader(CommandType.Text, strString);
                }catch(Exception ex)
                {

                    TraceLogHelper.Exception(strString, ex);
                }
            }
            else
            {
               
                DbCommand _cmd = _db.GetSqlStringCommand(strString);
                PrepareCommand(_cmd, parameters);
                try
                {
                    dtResult = _db.ExecuteReader(_cmd);
                }
                catch (Exception ex)
                {
                    TraceLogHelper.Exception(strString, parameters, ex);
                }
            }

            return dtResult;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public IDataReader ExecuteDataReaderByStoredProcedure(string storedProcedureName, params object[] parameterValues)
        {
            IDataReader oResult = null;
            if (_db == null) CreateDatabase();
            try
            {
                oResult = _db.ExecuteReader(storedProcedureName, parameterValues);
            }
            catch (Exception ex)
            {
                TraceLogHelper.Exception(storedProcedureName, parameterValues, ex);
            }
            return oResult;
        }
        #endregion
        #region DataTable

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strString)
        {
            return ExecuteDataTable(strString, null, null);
        }
        /// <summary>
        /// 返回DataTable，带有表名
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="parameters"></param>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strString, List<Parameter> parameters, string strTableName = null)
        {
            DataTable dtResult = null;

            if (_db == null) CreateDatabase();

            strString = PrepareSqlString(strString);
            if (parameters == null || parameters.Count == 0)
            {
                try
                {
                    dtResult = _db.ExecuteDataSet(CommandType.Text, strString).Tables[0];
                }
                catch (Exception ex)
                {
                    TraceLogHelper.Exception(strString, ex);
                }
            }
            else
            {
               
                DbCommand _cmd = _db.GetSqlStringCommand(strString);
                PrepareCommand(_cmd, parameters);
                try
                {
                    dtResult = _db.ExecuteDataSet(_cmd).Tables[0];
                }
                catch (Exception ex)
                {
                    TraceLogHelper.Exception(strString, parameters, ex);
                }
            }
            if (strTableName != null)
            {
                dtResult.TableName = strTableName;
            }
            return dtResult;
        }


        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTableByStoredProcedure(string storedProcedureName, params object[] parameterValues)
        {
            DataTable oResult = null;
            if (_db == null) CreateDatabase();
            try
            {
                oResult = _db.ExecuteDataSet(storedProcedureName, parameterValues).Tables[0];
            }
            catch (Exception ex)
            {
                TraceLogHelper.Exception(storedProcedureName, parameterValues, ex);
            }
            return oResult;
        }



        #endregion

        #region DataSet




        /// <summary>
        /// 返回DataSet,其中每个DataTable都带有名称
        /// </summary>
        /// <param name="strStrings"></param>
        /// <param name="strTableNames"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(List<string> strStrings, List<string> strTableNames = null)
        {
            DataSet dsResult = new DataSet();

            if (strTableNames == null || strTableNames.Count == 0)
            {
                for (int i = 0; i < strStrings.Count; i++)
                {
                    DataTable dtTmp = ExecuteDataTable(strStrings[i]);
                    dtTmp.TableName = i.ToString();
                    dsResult.Tables.Add(dtTmp.Copy());
                }
            }
            else
            {

                for (int i = 0; i < strStrings.Count; i++)
                {
                    DataTable dtTmp = ExecuteDataTable(strStrings[i]);
                    dtTmp.TableName = strTableNames[i];
                    dsResult.Tables.Add(dtTmp.Copy());
                }

            }


            return dsResult;
        }


        /// <summary>
        /// 获得多表集合，每个集合有对应的表名
        /// </summary>
        /// <param name="_lstSqlAndPara"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(List<SqlTextAndParameter> _lstSqlAndPara)
        {
            DataSet dsResult = new DataSet();

            foreach (SqlTextAndParameter _sqlTextAndPara in _lstSqlAndPara)
            {
                string strSqlString = _sqlTextAndPara.SqlString;
                string strTableName = _sqlTextAndPara.TableName;
                List<Parameter> _paras = _sqlTextAndPara.Parameters;
                DataTable dtTmp = ExecuteDataTable(strSqlString, _paras, strTableName);
                dsResult.Tables.Add(dtTmp.Copy());
            }
            return dsResult;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSetByStoredProcedure(string storedProcedureName, params object[] parameterValues)
        {
            DataSet oResult = null;
            if (_db == null) CreateDatabase();
            try
            {
                oResult = _db.ExecuteDataSet(storedProcedureName, parameterValues);
            }
            catch (Exception ex)
            {
                TraceLogHelper.Exception(storedProcedureName, parameterValues, ex);
            }
            return oResult;
        }
        #endregion


       
          /// <summary>
          ///  更新指定表数据
          ///  <para>说明：</para>   
          /// isSqlString 默认为false，TableNameOrSqlString为表名。如果为true，则TableNameOrSqlString应该传递"select key,col1,col2 from table"这样的sql语句,查询字段必须带有"主键列"。
          /// </summary>
        /// <param name="UpdateTable">DataTable结果集</param>
          /// <param name="TableNameOrSqlString">表名或SQL语句</param>
          /// <param name="isSqlString">是否为SQL语句标识：默认为false</param>
          /// <returns></returns>
        public bool UpdateTable(DataTable UpdateTable, string TableNameOrSqlString, bool isSqlString = false)
        {
            bool result = false;

            if (_db == null) CreateDatabase();
            DbDataAdapter da = _db.DbProviderFactory.CreateDataAdapter();
            DbCommand _cmd = _db.DbProviderFactory.CreateCommand();
            _cmd.Connection = _db.CreateConnection();
            _cmd.CommandText = isSqlString == true ? TableNameOrSqlString : "select * from " + TableNameOrSqlString;

            DbCommandBuilder builder = _db.DbProviderFactory.CreateCommandBuilder();
            //builder.ConflictOption = ConflictOption.OverwriteChanges;
            builder.DataAdapter = da;
            da.SelectCommand = _cmd;
            // Get the insert, update and delete commands.
            da.InsertCommand = builder.GetInsertCommand();
            da.UpdateCommand = builder.GetUpdateCommand();
            da.DeleteCommand = builder.GetDeleteCommand();
            da.Update(UpdateTable);
            result = true;

            return result;
        }



        #region Private Method

        private void CreateDatabase()
        {
            if (String.IsNullOrEmpty(this._Key))
            {
                _db = EnterpriseLibraryContainer.Current.GetInstance<Database>();
            }

            else
            {
                _db = EnterpriseLibraryContainer.Current.GetInstance<Database>(this._Key);
            }

            _DBType = _db.GetType().Name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cmd"></param>
        /// <param name="_Parameters"></param>
        private void PrepareCommand(DbCommand _cmd, List<Parameter> _Parameters)
        {
            if (_Parameters != null)
            {
                string strChar = "@";
                switch (_DBType)
                {
                    case "OracleDatabase":
                        strChar = ":";
                        break;
                    case "SqlDatabase":
                        strChar = "@";
                        break;
                    default:
                        break;
                }
                foreach (Parameter p in _Parameters)
                {
                    DbType dbtype = new DbType();
                    if (p.Value is DateTime)
                    {
                        dbtype = DbType.DateTime;
                    }
                    else
                    {
                        dbtype = DbType.AnsiString;
                    }
                    switch (p.Direction)
                    {
                        case ParameterDirection.Input:
                        case ParameterDirection.InputOutput:
                        case ParameterDirection.ReturnValue:
                        default:

                            _db.AddInParameter(_cmd, strChar + p.Key, dbtype, p.Value);
                            break;
                        case ParameterDirection.Output:
                            _db.AddOutParameter(_cmd, strChar + p.Key, dbtype, 100);
                            break;
                    }
                }
            }
        }

        private string PrepareSqlString(string _sqlstring)
        {
            if (string.IsNullOrEmpty(_sqlstring))
            {
                return string.Empty;
            }

            switch (_DBType)
            {
                case "OracleDatabase":
                    _sqlstring = _sqlstring.Replace(Convert.ToChar("@"), Convert.ToChar(":"));
                    _sqlstring = _sqlstring.Replace("ISNULL", "nvl");
                    _sqlstring = _sqlstring.Replace("GETDATE()", "(select sysdate from dual)");
                    break;
                //case "SqlDatabase":
                //    _sqlstring = _sqlstring.Replace(Convert.ToChar(":"), Convert.ToChar("@"));
                //    break;
                default:
                    break;
            }

            return _sqlstring;

        }

        #endregion
    }


    /// <summary>
    /// 
    /// </summary>
    public class DefaultDataBaseHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public static string GetDefaultDataBaseConnectStringName(string connectString = null)
        {

            DatabaseSettings databaseSettings = ConfigurationManager.GetSection("dataConfiguration") as DatabaseSettings;
            return databaseSettings.DefaultDatabase;
        }

    }
}
