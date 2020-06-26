using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    /// Data Transfer Object para a classe Usuarios do Domínio
    /// </summary>
    [Serializable]
    public class UsuariosDTO
    {
        public UsuariosDTO()
        {
            UsuarioId = Guid.NewGuid();
        }

        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
