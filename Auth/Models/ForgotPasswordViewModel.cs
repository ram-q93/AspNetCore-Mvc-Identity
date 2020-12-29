using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
