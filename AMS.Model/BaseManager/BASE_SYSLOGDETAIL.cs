using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_SYSLOGDETAIL
	///系统日志明细表
	///<summary>	
	[Serializable()]	
	[Description("系统日志明细表")] 
	public class BASE_SYSLOGDETAIL
	{
		/// <summary>
		/// 系统日志明细主键
		/// </summary>
	    [Description("系统日志明细主键")]
		public string syslogdetailid {get;set;}
		/// <summary>
		/// 日志主键
		/// </summary>
	    [Description("日志主键")]
		public string syslogid {get;set;}
		/// <summary>
		/// 属性名称
		/// </summary>
	    [Description("属性名称")]
		public string propertyname {get;set;}
		/// <summary>
		/// 属性字段
		/// </summary>
	    [Description("属性字段")]
		public string propertyfield {get;set;}
		/// <summary>
		/// 属性新值
		/// </summary>
	    [Description("属性新值")]
		public string newvalue {get;set;}
		/// <summary>
		/// 属性旧值
		/// </summary>
	    [Description("属性旧值")]
		public string oldvalue {get;set;}
		/// <summary>
		/// 创建时间
		/// </summary>
	    [Description("创建时间")]
		public DateTime? createdate {get;set;}
	}
}