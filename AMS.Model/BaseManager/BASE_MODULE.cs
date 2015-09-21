using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_MODULE
	///模块设置表
	///<summary>	
	[Serializable()]	
	[Description("模块设置表")] 
	public class BASE_MODULE
	{
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
		/// 分类
		/// </summary>
	    [Description("分类")]
		public string category {get;set;}
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
		/// 访问地址
		/// </summary>
	    [Description("访问地址")]
		public string location {get;set;}
		/// <summary>
		/// 目标
		/// </summary>
	    [Description("目标")]
		public string target {get;set;}
		/// <summary>
		/// 级别层次
		/// </summary>
	    [Description("级别层次")]
		public int? level {get;set;}
		/// <summary>
		/// 展开
		/// </summary>
	    [Description("展开")]
		public int? isexpand {get;set;}
		/// <summary>
		/// 动态按钮
		/// </summary>
	    [Description("动态按钮")]
		public int? allowbutton {get;set;}
		/// <summary>
		/// 动态视图
		/// </summary>
	    [Description("动态视图")]
		public int? allowview {get;set;}
		/// <summary>
		/// 动态表单
		/// </summary>
	    [Description("动态表单")]
		public int? allowform {get;set;}
		/// <summary>
		/// 访问权限
		/// </summary>
	    [Description("访问权限")]
		public int? authority {get;set;}
		/// <summary>
		/// 数据范围
		/// </summary>
	    [Description("数据范围")]
		public int? datascope {get;set;}
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