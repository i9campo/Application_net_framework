using System.ComponentModel.DataAnnotations;

namespace Sigma.Infra.CrossCutting.Identity.Model
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }


    public class RecPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name="Senha")]
        public string Password { get; set; }

        [Required]
        [Display(Name="Confire_Senha")]
        public string ConfirmPassword { get; set; }

    }
}
