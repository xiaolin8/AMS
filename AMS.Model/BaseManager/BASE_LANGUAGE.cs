using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_LANGUAGE
	///多语言表
	///<summary>	
	[Serializable()]	
	[Description("多语言表")] 
	public class BASE_LANGUAGE
	{
		/// <summary>
		/// 多语言主键
		/// </summary>
	    [Description("多语言主键")]
		public string languageid {get;set;}
		/// <summary>
		/// 对象ID
		/// </summary>
	    [Description("对象ID")]
		public string objectid {get;set;}
		/// <summary>
		/// 业务编码
		/// </summary>
	    [Description("业务编码")]
		public string businesscode {get;set;}
		/// <summary>
		/// 业务名称
		/// </summary>
	    [Description("业务名称")]
		public string businessname {get;set;}
		/// <summary>
		/// 名称
		/// </summary>
	    [Description("名称")]
		public string fullname {get;set;}
		/// <summary>
		/// 值
		/// </summary>
	    [Description("值")]
		public string fullvalue {get;set;}
		/// <summary>
		/// 注释
		/// </summary>
	    [Description("注释")]
		public string note {get;set;}
		/// <summary>
		/// 语言类型
		/// </summary>
	    [Description("语言类型")]
		public string languagetype {get;set;}
	}
}