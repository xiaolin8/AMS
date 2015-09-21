using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_DATASCOPEPERMISSION
	///数据范围权限表
	///<summary>	
	[Serializable()]	
	[Description("数据范围权限表")] 
	public class BASE_DATASCOPEPERMISSION
	{
		/// <summary>
		/// 数据范围权限主键
		/// </summary>
	    [Description("数据范围权限主键")]
		public string datascopepermissionid {get;set;}
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
		/// 对什么资源
		/// </summary>
	    [Description("对什么资源")]
		public string resourceid {get;set;}
		/// <summary>
		/// 自定义条件
		/// </summary>
	    [Description("自定义条件")]
		public string condition {get;set;}
		/// <summary>
		/// 自定义条件表单Json
		/// </summary>
	    [Description("自定义条件表单Json")]
		public string conditionjson {get;set;}
		/// <summary>
		/// 范围类型
		/// </summary>
	    [Description("范围类型")]
		public string scopetype {get;set;}
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