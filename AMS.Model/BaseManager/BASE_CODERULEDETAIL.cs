using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_CODERULEDETAIL
	///编码规则明细表
	///<summary>	
	[Serializable()]
	[Description("编码规则明细表")]
	public class BASE_CODERULEDETAIL
	{
		/// <summary>
		/// 编码规则明细主键
		/// </summary>
	    [Description("编码规则明细主键")]
		public string coderuledetailid {get;set;}
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
		/// 是否常量
		/// </summary>
	    [Description("是否常量")]
		public string consted {get;set;}
		/// <summary>
		/// 是否自动复位
		/// </summary>
	    [Description("是否自动复位")]
		public int? autoreset {get;set;}
		/// <summary>
		/// 是否定长
		/// </summary>
	    [Description("是否定长")]
		public int? fixlength {get;set;}
		/// <summary>
		/// 格式化字符串
		/// </summary>
	    [Description("格式化字符串")]
		public string formatstr {get;set;}
		/// <summary>
		/// 步长
		/// </summary>
	    [Description("步长")]
		public int? stepvalue {get;set;}
		/// <summary>
		/// 初始值
		/// </summary>
	    [Description("初始值")]
		public int? initvalue {get;set;}
		/// <summary>
		/// 长度
		/// </summary>
	    [Description("长度")]
		public int? flength {get;set;}
		/// <summary>
		/// 备注
		/// </summary>
	    [Description("备注")]
		public string remark {get;set;}
		/// <summary>
		/// 类型
		/// </summary>
	    [Description("类型")]
		public string ftype {get;set;}
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