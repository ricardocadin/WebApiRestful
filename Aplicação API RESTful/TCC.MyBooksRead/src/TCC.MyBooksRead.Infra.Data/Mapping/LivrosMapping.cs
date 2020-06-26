using System.Data.Entity.ModelConfiguration;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Infra.Data.Mapping
{
    /// <summary>
    /// Classe responsável por realizar as configurações de mapeamento das entidades para a base de dados
    /// </summary>
    public class LivrosMapping : EntityTypeConfiguration<Livros>
    {
        public LivrosMapping()
        {
            HasKey(x => x.LivroId);

            Property(x => x.Titulo).HasMaxLength(150).IsRequired();
            Property(x => x.AnoEdicao).HasColumnType("int").IsRequired();
            Property(x => x.Edicao).HasColumnType("int").IsRequired();
            Property(x => x.Editora).HasMaxLength(150).IsRequired();
            Property(x => x.ISBN).HasColumnType("int").IsOptional();
            Property(x => x.Idioma).HasMaxLength(150).IsOptional();
            Property(x => x.Pais).HasMaxLength(150).IsOptional();
            Property(x => x.QuantidadeCapitulos).HasColumnType("int").IsRequired();
            Property(x => x.QuantidadePaginas).HasColumnType("int").IsRequired();

            HasRequired(x => x.Usuario).WithMany(u => u.Livros).HasForeignKey(x => x.UsuarioId);

            HasRequired(x => x.Categorias).WithMany(c => c.Livros).HasForeignKey(c => c.CategoriaId);

            HasMany(x => x.Autores).WithMany(a => a.Livros)
                .Map(me =>
                {
                    me.MapLeftKey("LivrosId");
                    me.MapRightKey("AutoresId");
                    me.ToTable("LivrosPorAutores");
                });

            Ignore(x => x.ValidationResult);
        }
    }
}
