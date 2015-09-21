using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_CODERULE
	///编码规则主表
	///<summary>	
	[Serializable()]
	[Description("编码规则主表")]
	public class BASE_CODERULE
	{
		/// <summary>
		/// 编码规则主键
		/// </summary>
	    [Description("编码规则主键")]
		public string coderuleid {get;set;}
		/// <summary>
		/// 编码名称
		/// </summary>
	    [Description("编码名称")]
		public string fullname {get;set;}
		/// <summary>
		/// 编码代号
		/// </summary>
	    [Description("编码代号")]
		public string code {get;set;}
		/// <summary>
		/// 对应模块
		/// </summary>
	    [Description("对应模块")]
		public string moduleid {get;set;}
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