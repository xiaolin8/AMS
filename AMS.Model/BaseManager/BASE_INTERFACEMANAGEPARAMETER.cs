using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_INTERFACEMANAGEPARAMETER
	///接口参数
	///<summary>	
	[Serializable()]	
	[Description("接口参数")] 
	public class BASE_INTERFACEMANAGEPARAMETER
	{
		/// <summary>
		/// 接口参数主键
		/// </summary>
	    [Description("接口参数主键")]
		public string interfaceparameterid {get;set;}
		/// <summary>
		/// 接口主键
		/// </summary>
	    [Description("接口主键")]
		public string interfaceid {get;set;}
		/// <summary>
		/// 参数字段
		/// </summary>
	    [Description("参数字段")]
		public string field {get;set;}
		/// <summary>
		/// 参数说明
		/// </summary>
	    [Description("参数说明")]
		public string fieldmemo {get;set;}
		/// <summary>
		/// 参数类型
		/// </summary>
	    [Description("参数类型")]
		public string fieldtype {get;set;}
		/// <summary>
		/// 参数长度
		/// </summary>
	    [Description("参数长度")]
		public int? fieldmaxlength {get;set;}
		/// <summary>
		/// 允许空
		/// </summary>
	    [Description("允许空")]
		public int? allownull {get;set;}
		/// <summary>
		/// 有效
		/// </summary>
	    [Description("有效")]
		public int? enabled {get;set;}
		/// <summary>
		/// 排序码
		/// </summary>
	    [Description("排序码")]
		public int? sortcode {get;set;}
		/// <summary>
		/// 删除标记
		/// </summary>
	    [Description("删除标记")]
		public int? deletemark {get;set;}
	}
}