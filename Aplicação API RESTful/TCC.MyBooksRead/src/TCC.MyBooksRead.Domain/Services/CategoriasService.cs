using System;
using System.Collections.Generic;
using System.Linq;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Interfaces.Services;

namespace TCC.MyBooksRead.Domain.Services
{
    /// <summary>
    /// Categorias Domain Service - responsável por validar a entidade e persistir a mesma na base, 
    ///                         ele que orquestra as regras para tomar uma decisão.
    /// </summary>
    public class CategoriasService : ICategoriasService
    {
        private readonly ICategoriasRepository _categoriaRepository;

        public CategoriasService(ICategoriasRepository categoriasRepository)
        {
            _categoriaRepository = categoriasRepository;
        }

        /// <summary>
        /// Método responsável por buscar todas as categorias disponíveis no sistema
        /// </summary>
        /// <returns>Lista com as metas disponíveis</returns>
        public IEnumerable<Categorias> BuscarCategoriasDominio()
        {
            return _categoriaRepository.GetAll().OrderBy(x => x.Descricao);
        }

        /// <summary>
        /// Método responsável por limpar o contexto da memória
        /// </summary>
        public void Dispose()
        {
            _categoriaRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
