using System.ComponentModel.DataAnnotations;

namespace AMS.Entity
{
    public class AMS_GroupMemberIdModel
    {
        [Display(Name = "群成员Id")]
        public string member { get; set; }

        [Display(Name = "群主Id")]
        public string owner { get; set; }
    }
}