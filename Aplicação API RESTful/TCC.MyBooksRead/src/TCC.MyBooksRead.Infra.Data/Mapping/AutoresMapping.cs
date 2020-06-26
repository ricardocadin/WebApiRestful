using System.Data.Entity.ModelConfiguration;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Infra.Data.Mapping
{
    /// <summary>
    /// Classe responsável por realizar as configurações de mapeamento das entidades para a base de dados
    /// </summary>
    public class AutoresMapping : EntityTypeConfiguration<Autores>
    {
        public AutoresMapping()
        {
            HasKey(x => x.AutorId);

            Property(x => x.PrimeiroNome).HasMaxLength(150).IsRequired();
            Property(x => x.UltimoNome).HasMaxLength(150).IsOptional();
        }
    }
}
