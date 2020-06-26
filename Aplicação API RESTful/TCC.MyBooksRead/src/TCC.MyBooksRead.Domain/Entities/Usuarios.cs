using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Domain.Entities
{
    public class Usuarios
    {
        public Usuarios()
        {
            Livros = new List<Livros>();
            UsuarioId = Guid.NewGuid();
        }

        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual ICollection<Livros> Livros { get; set; }

    }
}
