using System;
using System.Collections.Generic;

namespace TCC.MyBooksRead.Domain.Entities
{
    /// <summary>
    /// Entidade Autor
    /// </summary>
    public class Autores
    {
        public Autores()
        {
            Livros = new List<Livros>();
            AutorId = Guid.NewGuid();

        }

        public Guid AutorId { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }

        public virtual ICollection<Livros> Livros { get; set; }
    }
}
