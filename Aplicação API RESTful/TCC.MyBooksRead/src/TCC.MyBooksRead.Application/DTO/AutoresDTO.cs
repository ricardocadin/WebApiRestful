using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    /// Data Transfer Object para a classe Autores do Domínio
    /// </summary>
    [Serializable]
    public class AutoresDTO
    {
        public AutoresDTO()
        {
            Livros = new List<LivrosDTO>();
            AutorId = Guid.NewGuid();

        }

        [Key]
        public Guid AutorId { get; set; }

        [Required(ErrorMessage = "Preencha o primeiro nome do Autor!")]
        [MaxLength(150, ErrorMessage = "Primeiro nome do Autor deve possuir no máximo {0} caracteres")]
        public string PrimeiroNome { get; set; }

        [MaxLength(150, ErrorMessage = "Segundo nome do Autor deve possuir no máximo {0} caracteres")]
        public string UltimoNome { get; set; }

        public ICollection<LivrosDTO> Livros { get; set; }
    }
}
