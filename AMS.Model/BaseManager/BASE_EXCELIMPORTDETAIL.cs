using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_EXCELIMPORTDETAIL
	///Excel导入表明细
	///<summary>	
	[Serializable()]	
	[Description("Excel导入表明细")] 
	public class BASE_EXCELIMPORTDETAIL
	{
		/// <summary>
		/// 导入列主键
		/// </summary>
	    [Description("导入列主键")]
		public string importdetailid {get;set;}
		/// <summary>
		/// 导入主键
		/// </summary>
	    [Description("导入主键")]
		public string importid {get;set;}
		/// <summary>
		/// Excel列名
		/// </summary>
	    [Description("Excel列名")]
		public string columnname {get;set;}
		/// <summary>
		/// 字段名
		/// </summary>
	    [Description("字段名")]
		public string fieldname {get;set;}
		/// <summary>
		/// 关联的外表
		/// </summary>
	    [Description("关联的外表")]
		public string foreigntable {get;set;}
		/// <summary>
		/// 外键字段
		/// </summary>
	    [Description("外键字段")]
		public string backfield {get;set;}
		/// <summary>
		/// 对比字段
		/// </summary>
	    [Description("对比字段")]
		public string comparefield {get;set;}
		/// <summary>
		/// 附加条件
		/// </summary>
	    [Description("附加条件")]
		public string attachcondition {get;set;}
		/// <summary>
		/// 数据类型
		/// </summary>
	    [Description("数据类型")]
		public int? datatype {get;set;}
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
		/// 字段备注
		/// </summary>
	    [Description("字段备注")]
		public string fieldremark {get;set;}
	}
}