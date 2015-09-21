Framework.DbUtility:

数据访问层


微软企业库操作数据库：以下情形

（1）复杂的逻辑对应的SQL语句
（2）存储过程操作


使用列子：以操作oracle数据库为列

！！！！！！！！！！！！！！！！！！
 （1）Oracle数据库： 使用new Parameter()增加参数时，如果表有Data类型，请加上DbType.DateTime
 parameters.Add(new Parameter("dtime", DateTime.Now.ToString(),DbType.DateTime));
 （2）SQLServver数据库： 使用new Parameter()增加参数时，如果表有Data类型，不需要请加上DbType.DateTime
 parameters.Add(new Parameter("dtime", DateTime.Now.ToString()));

！！！！！！！！！！！！！！！！

//执行单条查询SQL语句
DBHelper dbHelper = new DBHelper();
string strSql = "select '" + strUserPwd + "' as userpwd, N'王建' as username from dual";
DataTable _dataTable = dbHelper.ExecuteDataTable(strSql);


//带单条参数的查询SQL语句
DBHelper dbHelper = new DBHelper();
string strSql = "select * from dba_users where username=@username and  account_status=@status";
List<Parameter> parameters = new List<Parameter>();
parameters.Add(new Parameter("username", "SCOTT"));
parameters.Add(new Parameter("status", "OPEN"));
object obj = dbHelper.ExecuteScalar(strSql, parameters);

 
//执行多条SQL语句，SQL不带参数
string strSql = "insert into TEST_SAMPLE_A ([content]) values('wangjian')";
string strSql1 = "insert into TEST_SAMPLE_A ([content]) values('wangjian1')";
List<string> srrSqlString = new List<string>();
srrSqlString.Add(strSql);
srrSqlString.Add(strSql1);
int iResult = dbHelper.ExecuteNonQuery(srrSqlString);

///执行多条SQL语句，SQL都带参数

List<SqlTextAndParameter> lstSqlTextAndPara = new List<SqlTextAndParameter>();
SqlTextAndParameter _sqlTextAndPara = new SqlTextAndParameter();
_sqlTextAndPara.SqlString= "insert into TEST_SAMPLE_A ([content]) values(@content)";
_sqlTextAndPara.Parameters.Add(new Parameter("content", "wangjian3"));
lstSqlTextAndPara.Add(_sqlTextAndPara);


_sqlTextAndPara = new SqlTextAndParameter();
_sqlTextAndPara.SqlString = "insert into TEST_SAMPLE_A ([content]) values(@content)";
_sqlTextAndPara.Parameters.Add(new Parameter("content", "wangjian4"));
lstSqlTextAndPara.Add(_sqlTextAndPara);
int iResult = dbHelper.ExecuteNonQuery(lstSqlTextAndPara);



///执行多条SQL语句，部分SQL不都带参数

List<SqlTextAndParameter> lstSqlTextAndPara = new List<SqlTextAndParameter>();
SqlTextAndParameter _sqlTextAndPara = new SqlTextAndParameter();
_sqlTextAndPara.SqlString= "insert into TEST_SAMPLE_A ([content]) values(@content)";
_sqlTextAndPara.Parameters.Add(new Parameter("content", "wangjian3"));
lstSqlTextAndPara.Add(_sqlTextAndPara);


_sqlTextAndPara = new SqlTextAndParameter();
_sqlTextAndPara.SqlString = "insert into TEST_SAMPLE_A ([content]) values('wangjian4')";
lstSqlTextAndPara.Add(_sqlTextAndPara);
int iResult = dbHelper.ExecuteNonQuery(lstSqlTextAndPara);


//返回DataTable结果集合

dbHelper.ExecuteDataTable

//返回DataSet结果结合
dbHelper.ExecuteDataSet


-------------------------------------------------存储过程--------------------------


object[] ParamArray = new object[2];
ParamArray[0]="1";
ParamArray[1]="2";


dbHelper.ExecuteDataSetByStoredProcedure("storeProcedureName",ParamArray);



------------------------------------------Oracle 分页-----------------------------
string strSql="select * from table_a";
_DBHelper.ExecutePaginationProcedureOracle(strSql, pageIndex, pageSize, ref recordCount);