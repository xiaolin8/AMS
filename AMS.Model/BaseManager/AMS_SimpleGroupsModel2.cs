using System;
using System.ComponentModel.DataAnnotations;

namespace AMS.Entity
{
    [Serializable()]
    public class SimpleGroupsModel2
    {
        [Display(Name = "群主")]
        public string owner { get; set; }

        [Display(Name = "群ID")]
        public string groupid { get; set; }

        [Display(Name = "群人数")]
        public int affiliations { get; set; }

        [Display(Name = "群名称")]
        public string groupname { get; set; }

        public bool leaf { get; set; }
    }
}
