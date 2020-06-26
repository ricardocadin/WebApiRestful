using System;
using System.ComponentModel.DataAnnotations;
using ValidationResult = DomainValidation.Validation.ValidationResult;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    /// Data Transfer Object para a classe Metas do Domínio
    /// </summary>
    [Serializable]
    public class MetasLivrosDTO
    {
        public MetasLivrosDTO()
        {
            MetaId = Guid.NewGuid();
        }

        [Key]
        public Guid MetaId { get; set; }

        [Required(ErrorMessage = "Por favor, informe a descrição da meta a se cadastrar.")]
        [MaxLength(2000, ErrorMessage = "O campo descrição deve ter no máximo {0} caracteres.")]
        public string Descricao { get; set; }

        public bool MetaCumprida { get; set; }

        public DateTime? DataPrevista { get; set; }

        public DateTime? DataFim { get; set; }

        public DateTime DataCadastro { get; set; }

        public int PaginasDiarias { get; set; }

        public int CapitulosDiarios { get; set; }

        public int PaginasOuCapitulosLidos { get; set; }

        [Required(ErrorMessage = "Por favor, informe qual o identificador da meta que deseja cadastrar.")]
        public Guid MetaDominioId { get; set; }

        [Required(ErrorMessage = "Por favor, informe qual o identificador do livro que a meta pertence.")]
        public Guid LivroId { get; set; }

        public ValidationResult ValidationResult { get; set; }

        //Relacionamento com outros DTO
        public MetasExistentesDTO Metas { get; set; }
        public LivrosDTO Livros { get; set; }
    }
}
