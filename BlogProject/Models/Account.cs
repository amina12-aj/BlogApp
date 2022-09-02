using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Account
    {

        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ReturnUrl { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
