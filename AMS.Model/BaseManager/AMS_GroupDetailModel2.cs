using System.ComponentModel.DataAnnotations;

namespace AMS.Entity
{
    public class AMS_GroupDetailModel2
    {
        [Display(Name = "群ID")]
        public string id { get; set; }

        [Display(Name = "群名称")]
        public string name { get; set; }

        [Display(Name = "群描述")]
        public string description { get; set; }

        [Display(Name = "最大人数")]
        public int maxusers { get; set; }

        [Display(Name = "当前人数")]
        public int affiliations_count { get; set; }

        [Display(Name = "群成员Id")]
        public AMS_GroupMemberIdModel[] affiliations { get; set; }
    }
}