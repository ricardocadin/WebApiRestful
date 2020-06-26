using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.MyBooksRead.Application.DTO;

namespace TCC.MyBooksRead.Application.Interfaces
{
    public interface IMetasApplication : IDisposable
    {
        FiltrosPesquisaDTO<MetasLivrosDTO> MetasPorLivro(Guid livroId, int skip = 0, int take = 10);
        IEnumerable<MetasExistentesDTO> BuscarMetasDominio();
        MetasLivrosDTO Add(MetasLivrosDTO metasDto);
        MetasLivrosDTO GetById(Guid id);
        MetasLivrosDTO Update(MetasLivrosDTO metasDto);
        void Delete(Guid metaId);
    }
}
