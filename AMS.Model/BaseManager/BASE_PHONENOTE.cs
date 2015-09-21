using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_PHONENOTE
	///手机短信表
	///<summary>	
	[Serializable()]	
	[Description("手机短信表")] 
	public class BASE_PHONENOTE
	{
		/// <summary>
		/// 手机短信主键
		/// </summary>
	    [Description("手机短信主键")]
		public string phonenoteid {get;set;}
		/// <summary>
		/// 手机号码
		/// </summary>
	    [Description("手机号码")]
		public string phonennumber {get;set;}
		/// <summary>
		/// 发送内容
		/// </summary>
	    [Description("发送内容")]
		public string sendcontent {get;set;}
		/// <summary>
		/// 发送时间
		/// </summary>
	    [Description("发送时间")]
		public DateTime? sendtime {get;set;}
		/// <summary>
		/// 发送状态
		/// </summary>
	    [Description("发送状态")]
		public string sendstatus {get;set;}
		/// <summary>
		/// 设备名称
		/// </summary>
	    [Description("设备名称")]
		public string devicename {get;set;}
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
	}
}