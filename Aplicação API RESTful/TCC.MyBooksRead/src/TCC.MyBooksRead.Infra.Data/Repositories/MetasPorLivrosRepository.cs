using System;
using System.Collections.Generic;
using System.Linq;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;

namespace TCC.MyBooksRead.Infra.Data.Repositories
{
    public class MetasPorLivrosRepository : RepositoryBase<MetasPorLivros>, IMetasPorLivrosRepository
    {
        /// <summary>
        /// Obtém os livros de acordo com uma meta específica
        /// </summary>
        /// <param name="metaId">Identificador da meta</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public IEnumerable<MetasPorLivros> MetasPorLivro(Guid livroId, out int total, int skip = 0, int take = 10)
        {
            IQueryable<MetasPorLivros> metas = (IQueryable<MetasPorLivros>) base.GetAll("Livros");

            metas = metas.Where(x => x.LivroId == livroId);
            total = metas.Count();
            var metasRetorno = metas.ToList();
            return metasRetorno.Skip(skip).Take(take);
        }
    }
}
