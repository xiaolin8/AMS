using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_DEPARTMENT
	///部门管理表
	///<summary>	
	[Serializable()]	
	[Description("部门管理表")] 
	public class BASE_DEPARTMENT
	{
		/// <summary>
		/// 部门主键
		/// </summary>
	    [Description("部门主键")]
		public string departmentid {get;set;}
		/// <summary>
		/// 公司主键
		/// </summary>
	    [Description("公司主键")]
		public string companyid {get;set;}
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
		/// 简称
		/// </summary>
	    [Description("简称")]
		public string shortname {get;set;}
		/// <summary>
		/// 性质
		/// </summary>
	    [Description("性质")]
		public string nature {get;set;}
		/// <summary>
		/// 负责人
		/// </summary>
	    [Description("负责人")]
		public string manager {get;set;}
		/// <summary>
		/// 联系电话
		/// </summary>
	    [Description("联系电话")]
		public string phone {get;set;}
		/// <summary>
		/// 传真
		/// </summary>
	    [Description("传真")]
		public string fax {get;set;}
		/// <summary>
		/// 电子邮件
		/// </summary>
	    [Description("电子邮件")]
		public string email {get;set;}
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