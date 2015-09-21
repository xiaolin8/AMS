using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_COMPANY
	///公司管理表
	///<summary>
	[Serializable()]
	[Description("公司管理表")]
	public class BASE_COMPANY
	{
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
		/// 公司分类
		/// </summary>
	    [Description("公司分类")]
		public string category {get;set;}
		/// <summary>
		/// 公司编码
		/// </summary>
	    [Description("公司编码")]
		public string code {get;set;}
		/// <summary>
		/// 公司名称
		/// </summary>
	    [Description("公司名称")]
		public string fullname {get;set;}
		/// <summary>
		/// 公司简称
		/// </summary>
	    [Description("公司简称")]
		public string shortname {get;set;}
		/// <summary>
		/// 公司性质
		/// </summary>
	    [Description("公司性质")]
		public string nature {get;set;}
		/// <summary>
		/// 负责人
		/// </summary>
	    [Description("负责人")]
		public string manager {get;set;}
		/// <summary>
		/// 联系人
		/// </summary>
	    [Description("联系人")]
		public string contact {get;set;}
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
		/// 省主键
		/// </summary>
	    [Description("省主键")]
		public string provinceid {get;set;}
		/// <summary>
		/// 省
		/// </summary>
	    [Description("省")]
		public string province {get;set;}
		/// <summary>
		/// 市主键
		/// </summary>
	    [Description("市主键")]
		public string cityid {get;set;}
		/// <summary>
		/// 市
		/// </summary>
	    [Description("市")]
		public string city {get;set;}
		/// <summary>
		/// 县/区主键
		/// </summary>
	    [Description("县/区主键")]
		public string countyid {get;set;}
		/// <summary>
		/// 县/区
		/// </summary>
	    [Description("县/区")]
		public string county {get;set;}
		/// <summary>
		/// 详细地址
		/// </summary>
	    [Description("详细地址")]
		public string address {get;set;}
		/// <summary>
		/// 开户信息
		/// </summary>
	    [Description("开户信息")]
		public string accountinfo {get;set;}
		/// <summary>
		/// 邮编
		/// </summary>
	    [Description("邮编")]
		public string postalcode {get;set;}
		/// <summary>
		/// 网址
		/// </summary>
	    [Description("网址")]
		public string web {get;set;}
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
		/// <summary>
		/// 成立日期
		/// </summary>
	    [Description("成立日期")]
		public DateTime? begindate {get;set;}
		/// <summary>
		/// 撤消日期
		/// </summary>
	    [Description("撤消日期")]
		public DateTime? enddate {get;set;}
		/// <summary>
		/// 机构级别代码
		/// </summary>
	    [Description("机构级别代码")]
		public int? organizationtypecode {get;set;}
		/// <summary>
		/// 机构属性代码
		/// </summary>
	    [Description("机构属性代码")]
		public int? organizationpropertycode {get;set;}
	}
}