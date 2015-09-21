using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_VIEW
	///视图设置表
	///<summary>	
	[Serializable()]	
	[Description("视图设置表")] 
	public class BASE_VIEW
	{
		/// <summary>
		/// 视图主键
		/// </summary>
	    [Description("视图主键")]
		public string viewid {get;set;}
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
		/// <summary>
		/// 父级主键
		/// </summary>
	    [Description("父级主键")]
		public string parentid {get;set;}
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
		/// 显示列宽
		/// </summary>
	    [Description("显示列宽")]
		public int? columnwidth {get;set;}
		/// <summary>
		/// 对齐方式
		/// </summary>
	    [Description("对齐方式")]
		public string textalign {get;set;}
		/// <summary>
		/// 是否显示
		/// </summary>
	    [Description("是否显示")]
		public int? allowshow {get;set;}
		/// <summary>
		/// 导出/打印
		/// </summary>
	    [Description("导出/打印")]
		public int? allowderive {get;set;}
		/// <summary>
		/// 自定义转换
		/// </summary>
	    [Description("自定义转换")]
		public string customswitch {get;set;}
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
	}
}