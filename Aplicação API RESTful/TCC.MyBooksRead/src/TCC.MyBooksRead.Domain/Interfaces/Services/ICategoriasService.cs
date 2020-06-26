using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Domain.Interfaces.Services
{
    /// <summary>
    ///     Interface de contratado para os serviços disponíveis para comunicação com o domínio
    /// </summary>
    public interface ICategoriasService : IDisposable
    {
        IEnumerable<Categorias> BuscarCategoriasDominio();
    }
}
