using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Domain.Entities
{
    /// <summary>
    /// Entidade Categorias
    /// </summary>
    public class Categorias
    {
        public Categorias()
        {
            CategoriasDominioId = Guid.NewGuid();
            Livros = new List<Livros>();
        }

        public Guid CategoriasDominioId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Livros> Livros { get; set; } 
    }
}
