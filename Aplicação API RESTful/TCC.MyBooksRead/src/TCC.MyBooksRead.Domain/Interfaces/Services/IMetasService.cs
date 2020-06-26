using System;
using System.Collections.Generic;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Domain.Interfaces.Services
{
    /// <summary>
    ///     Interface de contratado para os serviços disponíveis para comunicação com o domínio
    /// </summary>
    public interface IMetasService : IDisposable
    {
        IEnumerable<MetasPorLivros> MetasPorLivro(Guid livroId, out int total, int skip = 0, int take = 10);
        IEnumerable<Metas> BuscarMetasDominio();
        MetasPorLivros Add(MetasPorLivros meta);
        MetasPorLivros GetById(Guid id);
        MetasPorLivros Update(MetasPorLivros meta);
        void Delete(Guid metaId);
    }
}
