using System.Data.Entity.ModelConfiguration;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Infra.Data.Mapping
{
    /// <summary>
    /// Classe responsável por realizar as configurações de mapeamento das entidades para a base de dados
    /// </summary>
    public class MetasMapping : EntityTypeConfiguration<Metas>
    {
        public MetasMapping()
        {
            HasKey(x => x.MetasDominioId);

            Property(x => x.Descricao).HasMaxLength(2000).HasColumnType("varchar").IsRequired();
        }
    }
}
