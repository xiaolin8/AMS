using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNet.Portal.Models
{
    public class GroupMemberIdModel
    {
        [Display(Name = "群成员Id")]
        public string member { get; set; }

        [Display(Name = "群主Id")]
        public string owner { get; set; }
    }
}