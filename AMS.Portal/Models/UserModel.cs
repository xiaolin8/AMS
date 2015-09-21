using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNet.Portal.Models
{
    public class UserModel
    {
        [Display(Name = "手机")]
        public string CellPhone { get; set; }
        [Display(Name = "备注")]
        public string Comment { get; set; }
        [Display(Name = "部门编号"), Required]
        public string DeptId { get; set; }
        [Display(Name = "序号")]
        public int DisplayIndex { get; set; }
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Display(Name = "英文名")]
        public string EnglishName { get; set; }
        [Display(Name = "性别")]
        public bool Gender { get; set; }
        [Display(Name = "头像")]
        public int HeadPic { get; set; }
        [Display(Name = "办公电话")]
        public string OfficePhone { get; set; }
        [Required, Display(Name = "职务")]
        public string Position { get; set; }
        [Display(Name = "签名")]
        public string Signature { get; set; }
        [Display(Name = "状态")]
        public int Status { get; set; }
        [Display(Name = "账号"), Required]
        public string UserId { get; set; }
        [Required, Display(Name = "用户名")]
        public string UserName { get; set; }

        [Display(Name = "是否群主")]
        public bool isOwner { get; set; }
        [Display(Name = "是否选中")]
        public bool active { get; set; }
    }
}