using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    /// Data Transfer Object contendo os filtros para realizar pesquisas
    /// </summary>
    [Serializable]
    public class PesquisaDTO
    {
        public string Titulo { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid MetaDominioId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
