using System.Data.Entity.ModelConfiguration;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Infra.Data.Mapping
{
    /// <summary>
    /// Classe responsável por realizar as configurações de mapeamento das entidades para a base de dados
    /// </summary>
    public class MetasPorLivrosMapping : EntityTypeConfiguration<MetasPorLivros>
    {
        public MetasPorLivrosMapping()
        {
            HasKey(x => x.MetaId);

            Property(x => x.Descricao).HasMaxLength(150).IsRequired();
            Property(x => x.MetaCumprida).HasColumnType("bit").IsRequired();
            Property(x => x.PaginasDiarias).IsOptional();
            Property(x => x.CapitulosDiarios).IsOptional();
            Property(x => x.PaginasOuCapitulosLidos).IsOptional();
            Property(x => x.DataPrevista).HasColumnType("datetime").IsOptional();
            Property(x => x.DataFim).HasColumnType("datetime").IsRequired();

            HasRequired(x => x.Livros).WithMany(l => l.Metas).HasForeignKey(x => x.LivroId);
            HasRequired(x => x.Metas).WithMany(m => m.MetasPorLivros).HasForeignKey(x => x.MetaDominioId);

            Ignore(x => x.ValidationResult);
        }
    }
}


