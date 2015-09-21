using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNet.Portal.Models
{
    public class DeptModel
    {
        public string DepId { get; set; }

        public string DepName { get; set; }

        public string ParDepId { get; set; }

        public int DisplayIndex { get; set; }

        [Display(Name = "部门主管")]
        public string DirectorId { get; set; }

        [Display(Name = "部门邮箱")]
        public string DepEmail { get; set; }

        [Display(Name = "状态")]
        public int Status { get; set; }

        public string Comment { get; set; }

        [Display(Name = "是否是叶子节点")]
        public bool IsLeaf { get; set; }
    }
}