using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_EMAILADDRESSEE
	///邮箱收件人表
	///<summary>	
	[Serializable()]	
	[Description("邮箱收件人表")] 
	public class BASE_EMAILADDRESSEE
	{
		/// <summary>
		/// 邮箱收件人主键
		/// </summary>
	    [Description("邮箱收件人主键")]
		public string emailaddresseeid {get;set;}
		/// <summary>
		/// 分类
		/// </summary>
	    [Description("分类")]
		public string category {get;set;}
		/// <summary>
		/// 邮件信息主键
		/// </summary>
	    [Description("邮件信息主键")]
		public string emailid {get;set;}
		/// <summary>
		/// 收件人主键
		/// </summary>
	    [Description("收件人主键")]
		public string addresseeid {get;set;}
		/// <summary>
		/// 收件人
		/// </summary>
	    [Description("收件人")]
		public string addresseename {get;set;}
		/// <summary>
		/// 状态: 0-收件;1-抄送;2-密送
		/// </summary>
	    [Description("状态: 0-收件;1-抄送;2-密送")]
		public int? addresseeidstate {get;set;}
		/// <summary>
		/// 是否阅读
		/// </summary>
	    [Description("是否阅读")]
		public int? isread {get;set;}
		/// <summary>
		/// 阅读次数
		/// </summary>
	    [Description("阅读次数")]
		public int? readcount {get;set;}
		/// <summary>
		/// 阅读日期
		/// </summary>
	    [Description("阅读日期")]
		public DateTime? readdate {get;set;}
		/// <summary>
		/// 最后阅读日期
		/// </summary>
	    [Description("最后阅读日期")]
		public DateTime? endreaddate {get;set;}
		/// <summary>
		/// 设置红旗
		/// </summary>
	    [Description("设置红旗")]
		public int? highlight {get;set;}
		/// <summary>
		/// 设置待办
		/// </summary>
	    [Description("设置待办")]
		public int? backlog {get;set;}
		/// <summary>
		/// 创建时间
		/// </summary>
	    [Description("创建时间")]
		public DateTime? createdate {get;set;}
		/// <summary>
		/// 删除标记
		/// </summary>
	    [Description("删除标记")]
		public int? deletemark {get;set;}
	}
}