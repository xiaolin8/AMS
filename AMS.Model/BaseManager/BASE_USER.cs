using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_USER
	///用户管理表
	///<summary>	
	[Serializable()]	
	[Description("用户管理表")] 
	public class BASE_USER
	{
		/// <summary>
		/// 用户主键
		/// </summary>
	    [Description("用户主键")]
		public string userid {get;set;}
		/// <summary>
		/// 公司主键
		/// </summary>
	    [Description("公司主键")]
		public string companyid {get;set;}
		/// <summary>
		/// 部门主键
		/// </summary>
	    [Description("部门主键")]
		public string departmentid {get;set;}
		/// <summary>
		/// 内部用户
		/// </summary>
	    [Description("内部用户")]
		public int? inneruser {get;set;}
		/// <summary>
		/// 用户编码
		/// </summary>
	    [Description("用户编码")]
		public string code {get;set;}
		/// <summary>
		/// 登录账户
		/// </summary>
	    [Description("登录账户")]
		public string account {get;set;}
		/// <summary>
		/// 登录密码
		/// </summary>
	    [Description("登录密码")]
		public string password {get;set;}
		/// <summary>
		/// 密码秘钥
		/// </summary>
	    [Description("密码秘钥")]
		public string secretkey {get;set;}
		/// <summary>
		/// 姓名
		/// </summary>
	    [Description("姓名")]
		public string realname {get;set;}
		/// <summary>
		/// 姓名拼音
		/// </summary>
	    [Description("姓名拼音")]
		public string spell {get;set;}
		/// <summary>
		/// 性别
		/// </summary>
	    [Description("性别")]
		public string gender {get;set;}
		/// <summary>
		/// 出生日期
		/// </summary>
	    [Description("出生日期")]
		public DateTime? birthday {get;set;}
		/// <summary>
		/// 手机
		/// </summary>
	    [Description("手机")]
		public string mobile {get;set;}
		/// <summary>
		/// 电话
		/// </summary>
	    [Description("电话")]
		public string telephone {get;set;}
		/// <summary>
		/// QQ号码
		/// </summary>
	    [Description("QQ号码")]
		public string oicq {get;set;}
		/// <summary>
		/// 电子邮件
		/// </summary>
	    [Description("电子邮件")]
		public string email {get;set;}
		/// <summary>
		/// 最后修改密码日期
		/// </summary>
	    [Description("最后修改密码日期")]
		public DateTime? changepassworddate {get;set;}
		/// <summary>
		/// 单点登录标识
		/// </summary>
	    [Description("单点登录标识")]
		public int? openid {get;set;}
		/// <summary>
		/// 登录次数
		/// </summary>
	    [Description("登录次数")]
		public int? logoncount {get;set;}
		/// <summary>
		/// 第一次访问时间
		/// </summary>
	    [Description("第一次访问时间")]
		public DateTime? firstvisit {get;set;}
		/// <summary>
		/// 上一次访问时间
		/// </summary>
	    [Description("上一次访问时间")]
		public DateTime? previousvisit {get;set;}
		/// <summary>
		/// 最后访问时间
		/// </summary>
	    [Description("最后访问时间")]
		public DateTime? lastvisit {get;set;}
		/// <summary>
		/// 审核状态
		/// </summary>
	    [Description("审核状态")]
		public string auditstatus {get;set;}
		/// <summary>
		/// 审核员主键
		/// </summary>
	    [Description("审核员主键")]
		public string audituserid {get;set;}
		/// <summary>
		/// 审核员
		/// </summary>
	    [Description("审核员")]
		public string auditusername {get;set;}
		/// <summary>
		/// 审核时间
		/// </summary>
	    [Description("审核时间")]
		public DateTime? auditdatetime {get;set;}
		/// <summary>
		/// 是否在线
		/// </summary>
	    [Description("是否在线")]
		public int? online {get;set;}
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