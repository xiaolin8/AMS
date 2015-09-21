using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_EMAIL
	///邮件信息表
	///<summary>	
	[Serializable()]	
	[Description("邮件信息表")] 
	public class BASE_EMAIL
	{
		/// <summary>
		/// 邮件信息主键
		/// </summary>
	    [Description("邮件信息主键")]
		public string emailid {get;set;}
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
		/// 主题
		/// </summary>
	    [Description("主题")]
		public string theme {get;set;}
		/// <summary>
		/// 色彩主题
		/// </summary>
	    [Description("色彩主题")]
		public string themecolour {get;set;}
		/// <summary>
		/// 内容
		/// </summary>
	    [Description("内容")]
		public string content {get;set;}
		/// <summary>
		/// 发件人
		/// </summary>
	    [Description("发件人")]
		public string addresser {get;set;}
		/// <summary>
		/// 发送日期
		/// </summary>
	    [Description("发送日期")]
		public DateTime? senddate {get;set;}
		/// <summary>
		/// 是否有附件
		/// </summary>
	    [Description("是否有附件")]
		public int? isaccessory {get;set;}
		/// <summary>
		/// 优先级
		/// </summary>
	    [Description("优先级")]
		public int? priority {get;set;}
		/// <summary>
		/// 需要回执
		/// </summary>
	    [Description("需要回执")]
		public int? receipt {get;set;}
		/// <summary>
		/// 是否定时发送
		/// </summary>
	    [Description("是否定时发送")]
		public int? isdelayed {get;set;}
		/// <summary>
		/// 定时时间
		/// </summary>
	    [Description("定时时间")]
		public DateTime? delayedtime {get;set;}
		/// <summary>
		/// 状态;1-已发送;0-草稿
		/// </summary>
	    [Description("状态;1-已发送;0-草稿")]
		public int? state {get;set;}
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
	}
}