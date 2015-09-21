using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_BUTTONPERMISSION
	///操作按钮权限表
	///<summary>	
	[Serializable()]	
	[Description("操作按钮权限表")] 
	public class BASE_BUTTONPERMISSION
	{
		/// <summary>
		/// 操作按钮权限主键
		/// </summary>
	    [Description("操作按钮权限主键")]
		public string buttonpermissionid {get;set;}
		/// <summary>
		/// 对象分类:1-部门2-角色3-岗位4-群组5-用户
		/// </summary>
	    [Description("对象分类:1-部门2-角色3-岗位4-群组5-用户")]
		public string category {get;set;}
		/// <summary>
		/// 对象主键
		/// </summary>
	    [Description("对象主键")]
		public string objectid {get;set;}
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
		/// <summary>
		/// 模块按钮主键
		/// </summary>
	    [Description("模块按钮主键")]
		public string modulebuttonid {get;set;}
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