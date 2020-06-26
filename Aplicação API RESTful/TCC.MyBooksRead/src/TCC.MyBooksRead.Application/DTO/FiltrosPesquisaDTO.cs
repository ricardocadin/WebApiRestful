using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.MyBooksRead.Application.DTO
{
    /// <summary>
    /// Classe criada a fim de utilizar a mesma para filtros de pesquisas que vierem da API de modo a reutilizar a mesma
    /// para diversos DTOs
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class FiltrosPesquisaDTO<TEntity> where TEntity : class
    {
        public FiltrosPesquisaDTO()
        {
            Dto = new List<TEntity>();
        }

        public ICollection<TEntity> Dto { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
    }
}
