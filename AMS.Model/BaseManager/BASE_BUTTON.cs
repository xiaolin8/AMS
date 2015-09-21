using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_BUTTON
	///按钮设置表
	///<summary>
	[Serializable()]
	[Description("按钮设置表")] 
	public class BASE_BUTTON
	{
		/// <summary>
		/// 按钮设置主键
		/// </summary>
	    [Description("按钮设置主键")]
		public string buttonid {get;set;}
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
		/// 编码
		/// </summary>
	    [Description("编码")]
		public string code {get;set;}
		/// <summary>
		/// 名称
		/// </summary>
	    [Description("名称")]
		public string fullname {get;set;}
		/// <summary>
		/// 图标
		/// </summary>
	    [Description("图标")]
		public string icon {get;set;}
		/// <summary>
		/// 分类
		/// </summary>
	    [Description("分类")]
		public string category {get;set;}
		/// <summary>
		/// js事件方法
		/// </summary>
	    [Description("js事件方法")]
		public string jsevent {get;set;}
		/// <summary>
		/// Action事件地址
		/// </summary>
	    [Description("Action事件地址")]
		public string actionevent {get;set;}
		/// <summary>
		/// 分开线
		/// </summary>
	    [Description("分开线")]
		public int? split {get;set;}
		/// <summary>
		/// 备注
		/// </summary>
	    [Description("备注")]
		public string remark {get;set;}
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
		/// 修改时间
		/// </summary>
	    [Description("修改时间")]
		public DateTime? modifydate {get;set;}
		/// <summary>
		/// 修改用户主键
		/// </summary>
	    [Description("修改用户主键")]
		public string modifyuserid {get;set;}
		/// <summary>
		/// 修改用户
		/// </summary>
	    [Description("修改用户")]
		public string modifyusername {get;set;}
	}
}