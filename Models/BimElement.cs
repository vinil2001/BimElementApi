
using System.ComponentModel.DataAnnotations;

namespace BimApi.Models
{
    public class BimElement
    {
        [Required]
        public string IfcGuid { get; set; }

        [Required]
        public string ElementType { get; set; }

        [Range(0, 100)]
        public int ProgressPercent { get; set; }

        public string Status
        {
            get
            {
                return ProgressPercent switch
                {
                    0 => "NotStarted",
                    100 => "Completed",
                    _ => "InProgress"
                };
            }
        }
    }
}
