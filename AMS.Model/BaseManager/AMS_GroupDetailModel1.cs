using System.ComponentModel.DataAnnotations;

namespace AMS.Entity
{
    public class AMS_GroupDetailModel1
    {
        [Display(Name = "群成员")]
        public AMS_GroupDetailModel2[] data { get; set; }
    }
}