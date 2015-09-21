using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_CODERULESERIOUS
	///编码规则种子表
	///<summary>	
	[Serializable()]	
	[Description("编码规则种子表")] 
	public class BASE_CODERULESERIOUS
	{
		/// <summary>
		/// 编码规则主键
		/// </summary>
	    [Description("编码规则主键")]
		public string coderuleid {get;set;}
		/// <summary>
		/// 用户主键
		/// </summary>
	    [Description("用户主键")]
		public string userid {get;set;}
		/// <summary>
		/// 种子值
		/// </summary>
	    [Description("种子值")]
		public int? nowvalue {get;set;}
		/// <summary>
		/// 种子类型（0-最大种子，1-用户占用种子）
		/// </summary>
	    [Description("种子类型（0-最大种子，1-用户占用种子）")]
		public string valuetype {get;set;}
		/// <summary>
		/// 有效(1-未使用，0-已使用)
		/// </summary>
	    [Description("有效(1-未使用，0-已使用)")]
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
		/// 上次更新日期
		/// </summary>
	    [Description("上次更新日期")]
		public string lastupdatedate {get;set;}
		/// <summary>
		/// 种子主键
		/// </summary>
	    [Description("种子主键")]
		public string codeseriousid {get;set;}
	}
}