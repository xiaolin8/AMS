using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_NETWORKFILE
	///网络硬盘文件表
	///<summary>	
	[Serializable()]	
	[Description("网络硬盘文件表")] 
	public class BASE_NETWORKFILE
	{
		/// <summary>
		/// 网络硬盘文件主键
		/// </summary>
	    [Description("网络硬盘文件主键")]
		public string networkfileid {get;set;}
		/// <summary>
		/// 文件夹主键
		/// </summary>
	    [Description("文件夹主键")]
		public string folderid {get;set;}
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
		/// 文件后缀名
		/// </summary>
	    [Description("文件后缀名")]
		public string fileextensions {get;set;}
		/// <summary>
		/// 文件类型
		/// </summary>
	    [Description("文件类型")]
		public string filetype {get;set;}
		/// <summary>
		/// 图标
		/// </summary>
	    [Description("图标")]
		public string icon {get;set;}
		/// <summary>
		/// 文件共享
		/// </summary>
	    [Description("文件共享")]
		public int? sharing {get;set;}
		/// <summary>
		/// 共享公共文件夹主键
		/// </summary>
	    [Description("共享公共文件夹主键")]
		public string sharingfolderid {get;set;}
		/// <summary>
		/// 共享开始时间
		/// </summary>
	    [Description("共享开始时间")]
		public DateTime? sharingcreatedate {get;set;}
		/// <summary>
		/// 共享结束时间
		/// </summary>
	    [Description("共享结束时间")]
		public DateTime? sharingenddate {get;set;}
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
	}
}