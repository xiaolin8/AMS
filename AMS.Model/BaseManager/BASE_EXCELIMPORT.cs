using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_EXCELIMPORT
	///Excel导入表
	///<summary>	
	[Serializable()]	
	[Description("Excel导入表")] 
	public class BASE_EXCELIMPORT
	{
		/// <summary>
		/// 导入主键
		/// </summary>
	    [Description("导入主键")]
		public string importid {get;set;}
		/// <summary>
		/// 模板编号
		/// </summary>
	    [Description("模板编号")]
		public string code {get;set;}
		/// <summary>
		/// 导入名称
		/// </summary>
	    [Description("导入名称")]
		public string importname {get;set;}
		/// <summary>
		/// 要导入的表
		/// </summary>
	    [Description("要导入的表")]
		public string importtable {get;set;}
		/// <summary>
		/// 表备注
		/// </summary>
	    [Description("表备注")]
		public string importtablename {get;set;}
		/// <summary>
		/// 导入Excel的文件名
		/// </summary>
	    [Description("导入Excel的文件名")]
		public string importfilename {get;set;}
		/// <summary>
		/// 遇到错误的处理机制：0-停止，1-跳过
		/// </summary>
	    [Description("遇到错误的处理机制：0-停止，1-跳过")]
		public int? errorhanding {get;set;}
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
		/// 对应模块
		/// </summary>
	    [Description("对应模块")]
		public string moduleid {get;set;}
	}
}