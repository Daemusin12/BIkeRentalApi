using System.ComponentModel.DataAnnotations;

namespace BIkeRentalApi.Models
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
