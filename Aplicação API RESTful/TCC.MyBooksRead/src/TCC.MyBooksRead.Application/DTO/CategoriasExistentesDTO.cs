using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    ///  Data Transfer Object contendo as categorias existentes no sistema
    /// </summary>
    [Serializable]
    public class CategoriasExistentesDTO
    {
        public CategoriasExistentesDTO()
        {
            Livros = new List<LivrosDTO>();
        }

        public Guid CategoriasDominioId { get; set; }
        public string Descricao { get; set; }

        public ICollection<LivrosDTO> Livros { get; set; } 
    }
}
