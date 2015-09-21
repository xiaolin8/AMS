using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_SHORTCUTS
	///首页快捷方式
	///<summary>	
	[Serializable()]	
	[Description("首页快捷方式")] 
	public class BASE_SHORTCUTS
	{
		/// <summary>
		/// 快捷方式主键
		/// </summary>
	    [Description("快捷方式主键")]
		public string shortcutsid {get;set;}
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
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
	}
}