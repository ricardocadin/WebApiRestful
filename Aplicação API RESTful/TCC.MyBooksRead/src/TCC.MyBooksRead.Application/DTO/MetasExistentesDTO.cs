using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    /// Data Transfer Object contendo as metas existentes no sistema
    /// </summary>
    [Serializable]
    public class MetasExistentesDTO
    {
        public MetasExistentesDTO()
        {
            MetasPorLivros = new List<MetasLivrosDTO>();
        }

        public Guid MetasDominioId { get; set; }
        public string Descricao { get; set; }

        public ICollection<MetasLivrosDTO> MetasPorLivros { get; set; } 
    }
}
