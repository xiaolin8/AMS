using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_OBJECTUSERRELATION
	///对象用户关系表
	///<summary>	
	[Serializable()]	
	[Description("对象用户关系表")] 
	public class BASE_OBJECTUSERRELATION
	{
		/// <summary>
		/// 对象用户关系主键
		/// </summary>
	    [Description("对象用户关系主键")]
		public string objectuserrelationid {get;set;}
		/// <summary>
		/// 对象分类:1-部门2-角色3-岗位4-群组
		/// </summary>
	    [Description("对象分类:1-部门2-角色3-岗位4-群组")]
		public string category {get;set;}
		/// <summary>
		/// 对象主键
		/// </summary>
	    [Description("对象主键")]
		public string objectid {get;set;}
		/// <summary>
		/// 用户主键
		/// </summary>
	    [Description("用户主键")]
		public string userid {get;set;}
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