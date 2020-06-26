using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Domain.Entities
{
    /// <summary>
    /// Entidade Metas
    /// </summary>
    public class Metas
    {
        public Metas()
        {
            MetasDominioId = Guid.NewGuid();
            MetasPorLivros = new List<MetasPorLivros>();
        }

        public Guid MetasDominioId { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<MetasPorLivros> MetasPorLivros { get; set; } 
    }
}
