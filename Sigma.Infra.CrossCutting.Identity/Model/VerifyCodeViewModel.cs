using System.ComponentModel.DataAnnotations;

namespace Sigma.Infra.CrossCutting.Identity.Model
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "C�digo")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Permanecer logado neste navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}