using System.ComponentModel.DataAnnotations;

namespace CES.CoreApi.Configuration.Model.Models
{
    public class SettingModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }

        public string Description { get; set; }
    }
}