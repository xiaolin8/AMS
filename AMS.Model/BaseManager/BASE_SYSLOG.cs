using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_SYSLOG
	///系统日志表
	///<summary>	
	[Serializable()]	
	[Description("系统日志表")] 
	public class BASE_SYSLOG
	{
		/// <summary>
		/// 日志主键
		/// </summary>
	    [Description("日志主键")]
		public string syslogid {get;set;}
		/// <summary>
		/// 对象主键
		/// </summary>
	    [Description("对象主键")]
		public string objectid {get;set;}
		/// <summary>
		/// 日志类型
		/// </summary>
	    [Description("日志类型")]
		public string logtype {get;set;}
		/// <summary>
		/// 操作IP
		/// </summary>
	    [Description("操作IP")]
		public string ipaddress {get;set;}
		/// <summary>
		/// IP地址所在地址
		/// </summary>
	    [Description("IP地址所在地址")]
		public string ipaddressname {get;set;}
		/// <summary>
		/// 公司主键
		/// </summary>
	    [Description("公司主键")]
		public string companyid {get;set;}
		/// <summary>
		/// 部门主键
		/// </summary>
	    [Description("部门主键")]
		public string departmentid {get;set;}
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
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
		/// <summary>
		/// 描述
		/// </summary>
	    [Description("描述")]
		public string remark {get;set;}
		/// <summary>
		/// 状态
		/// </summary>
	    [Description("状态")]
		public string status {get;set;}
	}
}