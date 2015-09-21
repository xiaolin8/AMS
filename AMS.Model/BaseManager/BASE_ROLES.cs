using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_ROLES
	///角色管理表
	///<summary>	
	[Serializable()]	
	[Description("角色管理表")] 
	public class BASE_ROLES
	{
		/// <summary>
		/// 角色主键
		/// </summary>
	    [Description("角色主键")]
		public string roleid {get;set;}
		/// <summary>
		/// 公司主键
		/// </summary>
	    [Description("公司主键")]
		public string companyid {get;set;}
		/// <summary>
		/// 角色分类
		/// </summary>
	    [Description("角色分类")]
		public string category {get;set;}
		/// <summary>
		/// 角色编码
		/// </summary>
	    [Description("角色编码")]
		public string code {get;set;}
		/// <summary>
		/// 角色名称
		/// </summary>
	    [Description("角色名称")]
		public string fullname {get;set;}
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