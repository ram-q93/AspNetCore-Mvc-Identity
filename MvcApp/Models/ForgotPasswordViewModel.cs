using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
