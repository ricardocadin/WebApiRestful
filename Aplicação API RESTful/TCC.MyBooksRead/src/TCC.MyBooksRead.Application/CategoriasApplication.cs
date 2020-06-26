using System;
using System.Collections.Generic;
using AutoMapper;
using TCC.MyBooksRead.Application.DTO;
using TCC.MyBooksRead.Application.Interfaces;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Services;

namespace TCC.MyBooksRead.Application
{
    /// <summary>
    /// Classe de Aplicação utilizada para orquestrar os serviços entre a API e o Domínio a fim de isolar o mesmo
    /// </summary>
    public class CategoriasApplication : ICategoriasApplication
    {
        private readonly ICategoriasService _categoriasService;

        public CategoriasApplication(ICategoriasService categoriasService)
        {
            _categoriasService = categoriasService;
        }

        /// <summary>
        /// Método responsável por retornar todas as categorias existentes no domínio da aplicação
        /// </summary>
        /// <returns>Lista de categorias</returns>
        public IEnumerable<CategoriasExistentesDTO> BuscarCategoriasDominio()
        {
            return Mapper.Map<IEnumerable<Categorias>, IEnumerable<CategoriasExistentesDTO>>(_categoriasService.BuscarCategoriasDominio());
        }

        /// <summary>
        /// Método responsável por limpar o contexto da memória
        /// </summary>
        public void Dispose()
        {
            _categoriasService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
