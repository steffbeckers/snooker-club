using System.ComponentModel.DataAnnotations;

namespace SC.API.ViewModels
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
