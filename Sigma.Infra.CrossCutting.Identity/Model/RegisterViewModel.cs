using System;
using System.ComponentModel.DataAnnotations;

namespace Sigma.Infra.CrossCutting.Identity.Model
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            IDCliente = Guid.NewGuid().ToString(); 
        }

        public String IDCliente { get; set; }

        [Required(ErrorMessage ="O nome � obrigat�rio")]
        [StringLength(100, ErrorMessage = "O nome deve ter pelo menos {2} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "O sobrenome � obrigat�rio")]
        //[StringLength(100, ErrorMessage = "O sobrenome deve ter pelo menos {2} caracteres.", MinimumLength = 3)]
        //[Display(Name = "Sobrenome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O email � obrigat�rio")]
        [EmailAddress(ErrorMessage ="O email n�o � v�lido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha � obrigat�ria")]
        [StringLength(100, ErrorMessage = "A senha deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Senha")]
        [Compare("Password", ErrorMessage = "As senhas n�o conferem!")]
        public string ConfirmPassword { get; set; }

        public bool Ativo { get; set;  }

        public bool Cliente { get; set; }
    }
}