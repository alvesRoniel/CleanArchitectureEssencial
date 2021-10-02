using System.ComponentModel.DataAnnotations;

namespace CleanArchMVC.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Invalid format email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
