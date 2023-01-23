using System.ComponentModel.DataAnnotations;

namespace restApi.Models
{
    public class History
    {
        [Key]
        public string city { get; set; }
        public string info { get; set; }
    }
}
