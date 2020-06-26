using System;
using System.Collections.Generic;
using DomainValidation.Validation;

namespace TCC.MyBooksRead.Domain.Entities
{
    /// <summary>
    /// Entidade Livro
    /// </summary>
    public class Livros
    {
        public Livros()
        {
            Autores = new List<Autores>();
            Metas = new List<MetasPorLivros>();
            LivroId = Guid.NewGuid();
        }

        public Guid LivroId { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int ISBN { get; set; }
        public string Idioma { get; set; }
        public int Edicao { get; set; }
        public int AnoEdicao { get; set; }
        public string Pais { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadePaginas { get; set; }
        public int QuantidadeCapitulos { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public virtual ICollection<Autores> Autores { get; set; }
        public virtual ICollection<MetasPorLivros> Metas { get; set; }
        public Guid CategoriaId { get; set; }
        public virtual Categorias Categorias { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual Usuarios Usuario { get; set; }

    }
}