using System;
using System.ComponentModel;
using DotNet.Utilities;

namespace AMS.Entity
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Description("用户管理")]
    [Key("UserId")]
    public class AMS_User
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [Description("用户主键")]
        public string UserId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        /// <returns></returns>
        [Description("登录账号")]
        public string Account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        /// <returns></returns>
        [Description("登录密码")]
        public string Password { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        /// <returns></returns>
        [Description("密钥")]
        public string Secretkey { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        [Description("姓名")]
        public string RealName { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        /// <returns></returns>
        [Description("别名")]
        public string Alias { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        /// <returns></returns>
        [Description("个性签名")]
        public string Signature { get; set; }

        /// <summary>
        /// 默认角色
        /// </summary>
        /// <returns></returns>
        [Description("默认角色")]
        public string RoleId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        [Description("性别")]
        public int Gender { get; set; }
        
        /// <summary>
        /// 公司主键
        /// </summary>
        /// <returns></returns>
        [Description("公司主键")]
        public string CompanyId { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>
        /// <returns></returns>
        [Description("部门主键")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 工作组主键
        /// </summary>
        /// <returns></returns>
        [Description("工作组主键")]
        public string WorkgroupId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        /// <returns></returns>
        [Description("头像")]
        public int HeadPic { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        [Description("手机")]
        public string Mobile { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        /// <returns></returns>
        [Description("办公电话")]
        public string OfficePhone { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        /// <returns></returns>
        [Description("身份证号码")]
        public string IDCard { get; set; }
        
        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        [Description("年龄")]
        public int? Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        /// <returns></returns>
        [Description("出生日期")]
        public DateTime? Birthday { get; set; }
        
        /// <summary>
        /// 工资卡
        /// </summary>
        /// <returns></returns>
        [Description("工资卡")]
        public string BankCode { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        /// <returns></returns>
        [Description("电子邮件")]
        public string Email { get; set; }

        /// <summary>
        /// QQ号码
        /// </summary>
        /// <returns></returns>
        [Description("QQ号码")]
        public string QQ { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        /// <returns></returns>
        [Description("最高学历")]
        public string Education { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        /// <returns></returns>
        [Description("毕业院校")]
        public string School { get; set; }

        /// <summary>
        /// 所学专业
        /// </summary>
        /// <returns></returns>
        [Description("所学专业")]
        public string Major { get; set; }

        /// <summary>
        /// 最高学位
        /// </summary>
        /// <returns></returns>
        [Description("最高学位")]
        public string Degree { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        /// <returns></returns>
        [Description("毕业时间")]
        public DateTime? GraduationDate { get; set; }

        /// <summary>
        /// 职位主键
        /// </summary>
        /// <returns></returns>
        [Description("职位主键")]
        public string DutyId { get; set; }

        /// <summary>
        /// 职称主键
        /// </summary>
        /// <returns></returns>
        [Description("职称主键")]
        public string TitleId { get; set; }

        /// <summary>
        /// 职称评定日期
        /// </summary>
        /// <returns></returns>
        [Description("职称评定日期")]
        public DateTime? TitleDate { get; set; }

        /// <summary>
        /// TitleLevel
        /// </summary>
        /// <returns></returns>
        [Description("TitleLevel")]
        public string TitleLevel { get; set; }

        /// <summary>
        /// 加入本单位时间
        /// </summary>
        /// <returns></returns>
        [Description("加入本单位时间")]
        public DateTime? JoinInDate { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        /// <returns></returns>
        [Description("籍贯")]
        public string NativePlace { get; set; }

        /// <summary>
        /// 离职
        /// </summary>
        /// <returns></returns>
        [Description("离职")]
        public int? IsDimission { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        /// <returns></returns>
        [Description("离职日期")]
        public DateTime? DimissionDate { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [Description("描述")]
        public string Description { get; set; }

        /// <summary>
        /// 最后修改密码日期
        /// </summary>
        /// <returns></returns>
        [Description("最后修改密码日期")]
        public DateTime? ChangePasswordDate { get; set; }

        /// <summary>
        /// 单点登录标识
        /// </summary>
        /// <returns></returns>
        [Description("单点登录标识")]
        public int? OpenId { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        /// <returns></returns>
        [Description("IP地址")]
        public string IPAddress { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        /// <returns></returns>
        [Description("登录次数")]
        public int? LogOnCount { get; set; }

        /// <summary>
        /// 第一次访问时间
        /// </summary>
        /// <returns></returns>
        [Description("第一次访问时间")]
        public DateTime? FirstVisit { get; set; }

        /// <summary>
        /// 上一次访问时间
        /// </summary>
        /// <returns></returns>
        [Description("上一次访问时间")]
        public DateTime? PreviousVisit { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        /// <returns></returns>
        [Description("最后访问时间")]
        public DateTime? LastVisit { get; set; }

        /// <summary>
        /// 有效：1-有效，0-无效
        /// </summary>
        /// <returns></returns>
        [Description("有效：1-有效，0-无效")]
        public int? Enabled { get; set; }

        /// <summary>
        /// 排序吗
        /// </summary>
        /// <returns></returns>
        [Description("排序吗")]
        public int? SortCode { get; set; }

        /// <summary>
        /// 删除标记:1-正常，0-删除
        /// </summary>
        /// <returns></returns>
        [Description("删除标记:1-正常，0-删除")]
        public int? DeleteMark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Description("创建时间")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Description("创建用户主键")]
        public string CreateUserId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Description("创建用户")]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Description("修改时间")]
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Description("修改用户主键")]
        public string ModifyUserId { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Description("修改用户")]
        public string ModifyUserName { get; set; }
    }
}