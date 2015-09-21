using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_EMAILACCESSORY
	///邮箱附件表
	///<summary>	
	[Serializable()]	
	[Description("邮箱附件表")] 
	public class BASE_EMAILACCESSORY
	{
		/// <summary>
		/// 邮箱附件主键
		/// </summary>
	    [Description("邮箱附件主键")]
		public string emailaccessoryid {get;set;}
		/// <summary>
		/// 邮件信息主键
		/// </summary>
	    [Description("邮件信息主键")]
		public string emailid {get;set;}
		/// <summary>
		/// 文件名称
		/// </summary>
	    [Description("文件名称")]
		public string filename {get;set;}
		/// <summary>
		/// 文件路径
		/// </summary>
	    [Description("文件路径")]
		public string filepath {get;set;}
		/// <summary>
		/// 文件大小
		/// </summary>
	    [Description("文件大小")]
		public string filesize {get;set;}
		/// <summary>
		/// 文件类型
		/// </summary>
	    [Description("文件类型")]
		public string filetype {get;set;}
		/// <summary>
		/// 创建时间
		/// </summary>
	    [Description("创建时间")]
		public DateTime? createdate {get;set;}
	}
}