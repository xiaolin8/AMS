using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_EMPLOYEE
	///职员信息表
	///<summary>	
	[Serializable()]	
	[Description("职员信息表")] 
	public class BASE_EMPLOYEE
	{
		/// <summary>
		/// 职员主键
		/// </summary>
	    [Description("职员主键")]
		public string employeeid {get;set;}
		/// <summary>
		/// 用户主键
		/// </summary>
	    [Description("用户主键")]
		public string userid {get;set;}
		/// <summary>
		/// 照片
		/// </summary>
	    [Description("照片")]
		public string photograph {get;set;}
		/// <summary>
		/// 身份证号码
		/// </summary>
	    [Description("身份证号码")]
		public string idcard {get;set;}
		/// <summary>
		/// 年龄
		/// </summary>
	    [Description("年龄")]
		public int? age {get;set;}
		/// <summary>
		/// 工资卡
		/// </summary>
	    [Description("工资卡")]
		public string bankcode {get;set;}
		/// <summary>
		/// 办公短号
		/// </summary>
	    [Description("办公短号")]
		public string officecornet {get;set;}
		/// <summary>
		/// 办公电话
		/// </summary>
	    [Description("办公电话")]
		public string officephone {get;set;}
		/// <summary>
		/// 办公邮编
		/// </summary>
	    [Description("办公邮编")]
		public string officezipcode {get;set;}
		/// <summary>
		/// 办公地址
		/// </summary>
	    [Description("办公地址")]
		public string officeaddress {get;set;}
		/// <summary>
		/// 办公传真
		/// </summary>
	    [Description("办公传真")]
		public string officefax {get;set;}
		/// <summary>
		/// 最高学历
		/// </summary>
	    [Description("最高学历")]
		public string education {get;set;}
		/// <summary>
		/// 毕业院校
		/// </summary>
	    [Description("毕业院校")]
		public string school {get;set;}
		/// <summary>
		/// 毕业时间
		/// </summary>
	    [Description("毕业时间")]
		public DateTime? graduationdate {get;set;}
		/// <summary>
		/// 所学专业
		/// </summary>
	    [Description("所学专业")]
		public string major {get;set;}
		/// <summary>
		/// 最高学位
		/// </summary>
	    [Description("最高学位")]
		public string degree {get;set;}
		/// <summary>
		/// 工作时间
		/// </summary>
	    [Description("工作时间")]
		public DateTime? workingdate {get;set;}
		/// <summary>
		/// 家庭住址邮编
		/// </summary>
	    [Description("家庭住址邮编")]
		public string homezipcode {get;set;}
		/// <summary>
		/// 家庭住址
		/// </summary>
	    [Description("家庭住址")]
		public string homeaddress {get;set;}
		/// <summary>
		/// 住宅电话
		/// </summary>
	    [Description("住宅电话")]
		public string homephone {get;set;}
		/// <summary>
		/// 家庭传真
		/// </summary>
	    [Description("家庭传真")]
		public string homefax {get;set;}
		/// <summary>
		/// 籍贯省
		/// </summary>
	    [Description("籍贯省")]
		public string province {get;set;}
		/// <summary>
		/// 籍贯市
		/// </summary>
	    [Description("籍贯市")]
		public string city {get;set;}
		/// <summary>
		/// 籍贯区
		/// </summary>
	    [Description("籍贯区")]
		public string area {get;set;}
		/// <summary>
		/// 籍贯
		/// </summary>
	    [Description("籍贯")]
		public string nativeplace {get;set;}
		/// <summary>
		/// 政治面貌
		/// </summary>
	    [Description("政治面貌")]
		public string party {get;set;}
		/// <summary>
		/// 国籍
		/// </summary>
	    [Description("国籍")]
		public string nation {get;set;}
		/// <summary>
		/// 民族
		/// </summary>
	    [Description("民族")]
		public string nationality {get;set;}
		/// <summary>
		/// 职务
		/// </summary>
	    [Description("职务")]
		public string duty {get;set;}
		/// <summary>
		/// 用工性质
		/// </summary>
	    [Description("用工性质")]
		public string workingproperty {get;set;}
		/// <summary>
		/// 职业资格
		/// </summary>
	    [Description("职业资格")]
		public string competency {get;set;}
		/// <summary>
		/// 紧急联系
		/// </summary>
	    [Description("紧急联系")]
		public string emergencycontact {get;set;}
		/// <summary>
		/// 离职
		/// </summary>
	    [Description("离职")]
		public int? isdimission {get;set;}
		/// <summary>
		/// 离职日期
		/// </summary>
	    [Description("离职日期")]
		public DateTime? dimissiondate {get;set;}
		/// <summary>
		/// 离职原因
		/// </summary>
	    [Description("离职原因")]
		public string dimissioncause {get;set;}
		/// <summary>
		/// 离职去向
		/// </summary>
	    [Description("离职去向")]
		public string dimissionwhither {get;set;}
	}
}