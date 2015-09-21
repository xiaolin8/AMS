using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DotNet.Portal.Models;

namespace DotNet.Portal.Controllers
{
    [Serializable()]
    public class GroupDetailModel1
    {
        [Display(Name = "群成员")]
        public GroupDetailModel2[] data { get; set; }
    }
}