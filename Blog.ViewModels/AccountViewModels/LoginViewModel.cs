using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Invalid")]
        public string Password { get; set; }
    }
}
