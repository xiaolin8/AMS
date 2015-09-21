using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_FORMATTRIBUTE
	///表单附加属性
	///<summary>	
	[Serializable()]	
	[Description("表单附加属性")] 
	public class BASE_FORMATTRIBUTE
	{
		/// <summary>
		/// 表单附加属性主键
		/// </summary>
	    [Description("表单附加属性主键")]
		public string formattributeid {get;set;}
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
		/// <summary>
		/// 属性名称
		/// </summary>
	    [Description("属性名称")]
		public string propertyname {get;set;}
		/// <summary>
		/// 控件Id
		/// </summary>
	    [Description("控件Id")]
		public string controlid {get;set;}
		/// <summary>
		/// 控件类型
		/// </summary>
	    [Description("控件类型")]
		public string controltype {get;set;}
		/// <summary>
		/// 控件样式
		/// </summary>
	    [Description("控件样式")]
		public string controlstyle {get;set;}
		/// <summary>
		/// 控件验证
		/// </summary>
	    [Description("控件验证")]
		public string controlvalidator {get;set;}
		/// <summary>
		/// 输入长度
		/// </summary>
	    [Description("输入长度")]
		public int? importlength {get;set;}
		/// <summary>
		/// 默认值
		/// </summary>
	    [Description("默认值")]
		public string defaultvlaue {get;set;}
		/// <summary>
		/// 自定义属性
		/// </summary>
	    [Description("自定义属性")]
		public string attributesproperty {get;set;}
		/// <summary>
		/// 控件数据源类型
		/// </summary>
	    [Description("控件数据源类型")]
		public int? datasourcetype {get;set;}
		/// <summary>
		/// 控件数据源
		/// </summary>
	    [Description("控件数据源")]
		public string datasource {get;set;}
		/// <summary>
		/// 合并列
		/// </summary>
	    [Description("合并列")]
		public string controlcolspan {get;set;}
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