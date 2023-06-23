using Sigma.Domain.IdentityEntities;
using System.Data.Entity.ModelConfiguration;
namespace Sigma.Infra.Data.Context.MapConfig
{
    public class UsuarioAtivoConfig : EntityTypeConfiguration<UsuarioAtivo>
    {
        public UsuarioAtivoConfig()
        {
            Property(o => o.Ativo)
           .IsRequired();

            HasRequired(o => o.Empresa)
            .WithMany(o => o.UsuarioAtivo)
            .HasForeignKey(o => o.IDEmpresa);

            HasRequired(o => o.Usuario)
            .WithMany(o => o.UsuarioAtivo)
            .HasForeignKey(o => o.IDUsuario);
        }
    }
}
