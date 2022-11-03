using System.ComponentModel.DataAnnotations;

namespace MyMealRestaurant.Models.DTOs
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
