using System;
using System.ComponentModel;
namespace AMS.Entity
{
 	///<summary>
	///BASE_NETWORKFOLDER
	///网络硬盘文件夹表
	///<summary>	
	[Serializable()]	
	[Description("网络硬盘文件夹表")] 
	public class BASE_NETWORKFOLDER
	{
		/// <summary>
		/// 文件夹主键
		/// </summary>
	    [Description("文件夹主键")]
		public string folderid {get;set;}
		/// <summary>
		/// 父级主键
		/// </summary>
	    [Description("父级主键")]
		public string parentid {get;set;}
		/// <summary>
		/// 分类
		/// </summary>
	    [Description("分类")]
		public string category {get;set;}
		/// <summary>
		/// 文件夹
		/// </summary>
	    [Description("文件夹")]
		public string foldername {get;set;}
		/// <summary>
		/// 是公开
		/// </summary>
	    [Description("是公开")]
		public int? ispublic {get;set;}
		/// <summary>
		/// 文件夹共享
		/// </summary>
	    [Description("文件夹共享")]
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