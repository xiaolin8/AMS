using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_FORMATTRIBUTEVALUE
	///表单附加属性实例
	///<summary>	
	[Serializable()]	
	[Description("表单附加属性实例")] 
	public class BASE_FORMATTRIBUTEVALUE
	{
		/// <summary>
		/// 附加属性实例主键
		/// </summary>
	    [Description("附加属性实例主键")]
		public string attributevalueid {get;set;}
		/// <summary>
		/// 模块主键
		/// </summary>
	    [Description("模块主键")]
		public string moduleid {get;set;}
		/// <summary>
		/// 对象主键
		/// </summary>
	    [Description("对象主键")]
		public string objectid {get;set;}
		/// <summary>
		/// 参数Json
		/// </summary>
	    [Description("参数Json")]
		public string objectparameterjson {get;set;}
	}
}