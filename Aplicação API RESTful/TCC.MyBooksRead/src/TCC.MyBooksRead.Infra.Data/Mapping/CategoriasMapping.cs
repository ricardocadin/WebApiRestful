using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Infra.Data.Mapping
{
    /// <summary>
    /// Classe responsável por realizar as configurações de mapeamento das entidades para a base de dados
    /// </summary>
    public class CategoriasMapping : EntityTypeConfiguration<Categorias>
    {
        public CategoriasMapping()
        {
            HasKey(x => x.CategoriasDominioId);

            Property(x => x.Descricao).HasMaxLength(150).HasColumnType("varchar").IsRequired();
        }
    }
}
