using System;
namespace Sigma.Domain.ViewTables
{
    public class UserView
    {
        public Guid? IDEmpresa { get; set; }
        
        public String Id { get; set; }
        public String Name { get; set; }
        public String Nome { get; set; }
        public String Sobrenome { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
        public String UserId { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public String ConfirmPassword { get; set; }
        public String Token { get; set; }
        public String PasswordHash { get; set; }
        public String SecurityStamp { get; set; }
        public String PhoneNumber { get; set; }
        public String RoleId { get; set; }
        public String ViewerRoler { get; set; }
        public String ClaimValue { get; set; }
        public String Tipo { get; set; }
        public String Checado { get; set; }
        public String CheckedViewer { get; set; }
        public String NomeEmpresa { get; set; }
        public String TipoUsuario { get; set; }

        public Boolean? PhoneNumberConfirmed { get; set; }
        public Boolean? TwoFactorEnabled { get; set; }
        public Boolean? EmailConfirmed { get; set; }
        public Boolean? LockoutEnabled { get; set; }
        public Boolean? Ativo { get; set; }
        public Boolean? Conectado { get; set; }
        public Boolean? Checked { get; set; }


        public int? IDFirstName { get; set; }
        public int? IDLastName { get; set; }
        public int? AccessFailedCount { get; set; }


        public DateTime? LockoutEndDateUtc { get; set; }
    }
}
