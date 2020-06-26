using System;
using System.Collections.Generic;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Domain.Interfaces.Repository
{
    public interface IMetasPorLivrosRepository : IRepositoryBase<MetasPorLivros>
    {
        IEnumerable<MetasPorLivros> MetasPorLivro(Guid livroId, out int total, int skip = 0, int take = 10);
    }
}
