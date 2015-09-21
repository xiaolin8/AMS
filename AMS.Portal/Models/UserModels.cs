using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AndroidMvcServer.Portal.Models
{
    public class UserModels
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string UserPassword { get; set; }

        [Required]
        [Display(Name = "账号")]
        public string UserId { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        [Required]
        [Display(Name = "性别")]
        public int Gender { get; set; }

        [Display(Name = "头像")]
        public Byte[] Photo { get; set; }

        [Display(Name = "签名")]
        public string Signature { get; set; }

        [Display(Name = "手机")]
        public string CellPhone { get; set; }

        [Display(Name = "办公电话")]
        public string OfficePhone { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "部门编号")]
        public string DeptId { get; set; }

        [Required]
        [Display(Name = "职务")]
        public string Position { get; set; }

        [Display(Name = "序号")]
        public int DisplayIndex { get; set; }
    }
}