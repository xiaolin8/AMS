using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DotNet.Portal.Models;

namespace DotNet.Portal.Models
{
    [Serializable()]
    public class SimpleGroupsModel1
    {
        [Display(Name = "群成员")]
        public SimpleGroupsModel2[] data { get; set; }

        [Display(Name = "群总数")]
        public int count { get; set; }
    }
}