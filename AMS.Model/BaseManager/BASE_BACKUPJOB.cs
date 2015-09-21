using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_BACKUPJOB
	///数据库备份计划表
	///<summary>	
	[Serializable()]	
	[Description("数据库备份计划表")] 
	public class BASE_BACKUPJOB
	{
		/// <summary>
		/// 备份主键
		/// </summary>
	    [Description("备份主键")]
		public string backupid {get;set;}
		/// <summary>
		/// 服务器地址
		/// </summary>
	    [Description("服务器地址")]
		public string servername {get;set;}
		/// <summary>
		/// 数据库
		/// </summary>
	    [Description("数据库")]
		public string dbname {get;set;}
		/// <summary>
		/// 计划名称
		/// </summary>
	    [Description("计划名称")]
		public string jobname {get;set;}
		/// <summary>
		/// 执行方式
		/// </summary>
	    [Description("执行方式")]
		public string mode {get;set;}
		/// <summary>
		/// 每天启动时间
		/// </summary>
	    [Description("每天启动时间")]
		public string starttime {get;set;}
		/// <summary>
		/// 备份路径
		/// </summary>
	    [Description("备份路径")]
		public string filepath {get;set;}
		/// <summary>
		/// 备注
		/// </summary>
	    [Description("备注")]
		public string remark {get;set;}
		/// <summary>
		/// 有效
		/// </summary>
	    [Description("有效")]
		public string enabled {get;set;}
		/// <summary>
		/// 创建时间
		/// </summary>
	    [Description("创建时间")]
		public DateTime? createdate {get;set;}
		/// <summary>
		/// 创建用户主键
		/// </summary>
	    [Description("创建用户主键")]
		public string createuserid {get;set;}
		/// <summary>
		/// 创建用户
		/// </summary>
	    [Description("创建用户")]
		public string createusername {get;set;}
	}
}