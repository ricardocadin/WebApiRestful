using System.Data.Entity.ModelConfiguration;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Infra.Data.Mapping
{
    /// <summary>
    /// Classe responsável por realizar as configurações de mapeamento das entidades para a base de dados
    /// </summary>
    public class UsuariosMapping : EntityTypeConfiguration<Usuarios>
    {
        public UsuariosMapping()
        {
            HasKey(x => x.UsuarioId);

            Property(x => x.Nome).HasMaxLength(150).IsRequired();
            Property(x => x.Sobrenome).HasMaxLength(150).IsRequired();
            Property(x => x.Email).HasMaxLength(150).IsRequired();
            Property(x => x.CPF).HasMaxLength(11).IsRequired();
        }
    }
}
