using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_VIEWWHERE
	///视图查询条件表
	///<summary>	
	[Serializable()]	
	[Description("视图查询条件表")] 
	public class BASE_VIEWWHERE
	{
		/// <summary>
		/// 视图查询条件主键
		/// </summary>
	    [Description("视图查询条件主键")]
		public string viewwhereid {get;set;}
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
		/// <summary>
		/// 控件类型
		/// </summary>
	    [Description("控件类型")]
		public string controltype {get;set;}
		/// <summary>
		/// 控件默认值
		/// </summary>
	    [Description("控件默认值")]
		public string controldefault {get;set;}
		/// <summary>
		/// 绑定数据源
		/// </summary>
	    [Description("绑定数据源")]
		public string controlsource {get;set;}
		/// <summary>
		/// 字段名称
		/// </summary>
	    [Description("字段名称")]
		public string fieldname {get;set;}
		/// <summary>
		/// 内部名称
		/// </summary>
	    [Description("内部名称")]
		public string fullname {get;set;}
		/// <summary>
		/// 显示名称
		/// </summary>
	    [Description("显示名称")]
		public string showname {get;set;}
		/// <summary>
		/// 是否显示
		/// </summary>
	    [Description("是否显示")]
		public int? allowshow {get;set;}
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
		/// 创建时间
		/// </summary>
	    [Description("创建时间")]
		public DateTime? createdate {get;set;}
		/// <summary>
		/// 自定义
		/// </summary>
	    [Description("自定义")]
		public string controlcustom {get;set;}
	}
}