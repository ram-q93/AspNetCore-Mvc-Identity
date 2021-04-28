using System.ComponentModel.DataAnnotations;

namespace MvcApp.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
