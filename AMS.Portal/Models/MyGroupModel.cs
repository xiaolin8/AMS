using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNet.Portal.Models
{
    public class MyGroupModel
    {
        [Display(Name = "群ID")]
        public string groupId { get; set; }

        [Display(Name = "群名")]
        public string groupName { get; set; }

        [Display(Name = "群主")]
        public string groupOwner { get; set; }

        [Display(Name = "群简介")]
        public string groupDesc { get; set; }

        [Display(Name = "是否公开")]
        public bool isPublic { get; set; }

        [Display(Name = "是否允许成员邀请")]
        public bool allowInvite { get; set; }

        [Display(Name = "总人数")]
        public int memberNum { get; set; }

        [Display(Name = "最大人数")]
        public int maxUsers { get; set; }
    }
}