using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_QUERYRECORD
	///查询条件记录
	///<summary>	
	[Serializable()]	
	[Description("查询条件记录")] 
	public class BASE_QUERYRECORD
	{
		/// <summary>
		/// 查询条件记录主键
		/// </summary>
	    [Description("查询条件记录主键")]
		public string queryrecordid {get;set;}
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
		/// <summary>
		/// 方案名称
		/// </summary>
	    [Description("方案名称")]
		public string fullname {get;set;}
		/// <summary>
		/// 条件JSON格式
		/// </summary>
	    [Description("条件JSON格式")]
		public string conditionjson {get;set;}
		/// <summary>
		/// 共享（只读）
		/// </summary>
	    [Description("共享（只读）")]
		public int? resourceshare {get;set;}
		/// <summary>
		/// 下次默认
		/// </summary>
	    [Description("下次默认")]
		public int? nextdefault {get;set;}
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