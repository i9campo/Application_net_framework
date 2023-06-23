using System;
using Sigma.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Sigma.Infra.Data.MapConfig
{
    public class ProprietarioConfig : EntityTypeConfiguration<Proprietario>
    {
        public ProprietarioConfig()
        {
            Property(o => o.nome)
                .IsRequired()
                .HasMaxLength(75);

            HasRequired(o => o.Empresa)
             .WithMany(o => o.Proprietario)
             .HasForeignKey(o => o.IDEmpresa);

            Property(o => o.tipoProprietario)
                .HasMaxLength(4);

            Property(o => o.pfpj)
                .IsRequired()
                .HasMaxLength(18);

            Property(o => o.cidade)
                .IsRequired()
                .HasMaxLength(50);

            Property(o => o.uf)
                .IsRequired()
                .HasMaxLength(2);

            Property(o => o.endereco)
             .HasMaxLength(150);


            Property(o => o.cep)
                .HasMaxLength(9);

            Property(o => o.fone)
                .HasMaxLength(17);

            Property(o => o.email)
                .HasMaxLength(50);

            Property(o => o.infoAdicionais)
                .HasMaxLength(int.MaxValue);

            Property(o => o.representante)
                .HasMaxLength(75);

            Property(o => o.cpfRepresentante)
                .HasMaxLength(14);

            Property(o => o.telefoneRepresentante)
                .HasMaxLength(17);

        }
    }
}
