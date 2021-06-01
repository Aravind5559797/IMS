using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models
{
    public class Platform
    {
        [DisplayName("Platform Name")]
        [Key]
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string Image { get; set; }
    }
}