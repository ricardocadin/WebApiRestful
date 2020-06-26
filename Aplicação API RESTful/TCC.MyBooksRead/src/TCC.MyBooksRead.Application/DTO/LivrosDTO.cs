using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationResult = DomainValidation.Validation.ValidationResult;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    /// Data Transfer Object para classe Livros do Domínio
    /// </summary>
    [Serializable]
    public class LivrosDTO
    {
        public LivrosDTO()
        {
            LivroId = Guid.NewGuid();
            Autores = new List<AutoresDTO>();
            Metas = new List<MetasLivrosDTO>();
        }

        [Key]
        public Guid LivroId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o título do livro.")]
        [MaxLength(150, ErrorMessage = "O campo título deve ter no máximo {0} caracteres.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Por favor, informe a editora do livro.")]
        [MaxLength(150, ErrorMessage = "O campo Editora deve ter no máximo {0} caracteres.")]
        public string Editora { get; set; }

        public string Idioma { get; set; }

        [Required(ErrorMessage = "Por favor, informe a edição do livro.")]
        [Range(1, 20, ErrorMessage = "O campo edição deve conter um valor de 1 a 20.")]
        public int Edicao { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ano de publicação do livro.")]
        [Range(1900, 2050, ErrorMessage = "O ano da edição deve conter um valor de 1900 a 2050.")]
        public int AnoEdicao { get; set; }

        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade de páginas do livro.")]
        public int QuantidadePaginas { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade de capítulos do livro.")]
        public int QuantidadeCapitulos { get; set; }

        public ValidationResult ValidationResult { get; set; }

        [Required(ErrorMessage = "Por favor, informe qual a categoria do livro que deseja cadastrar.")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o identificador do usuário para cadastrar.")]
        public Guid UsuarioId { get; set; }

        //Relacionamentos com outros DTOs
        public ICollection<AutoresDTO> Autores { get; set; }
        public ICollection<MetasLivrosDTO> Metas { get; set; }
        public CategoriasExistentesDTO Categorias { get; set; }
        public UsuariosDTO Usuario { get; set; }

    }
}
