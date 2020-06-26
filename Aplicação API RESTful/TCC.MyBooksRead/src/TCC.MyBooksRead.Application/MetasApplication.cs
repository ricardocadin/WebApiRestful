using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TCC.MyBooksRead.Application.DTO;
using TCC.MyBooksRead.Application.Interfaces;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Interfaces.Services;

namespace TCC.MyBooksRead.Application
{
    /// <summary>
    /// Classe de Aplicação utilizada para orquestrar os serviços entre a API e o Domínio a fim de isolar o mesmo
    /// </summary>
    public class MetasApplication : IMetasApplication
    {
        private readonly IMetasService _metasService;

        public MetasApplication(IMetasService metasService)
        {
            _metasService = metasService;
        }

        /// <summary>
        /// Método responsável por buscar todas as metas cadastradas pertencentes a um livro
        /// </summary>
        /// <param name="livroId">Identificador do livro</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns></returns>
        public FiltrosPesquisaDTO<MetasLivrosDTO> MetasPorLivro(Guid livroId, int skip = 0, int take = 10)
        {
            var total = 0;
            var metasRegistradas = _metasService.MetasPorLivro(livroId, out total, skip, take);
            return ConverterListaDominioToDto(metasRegistradas, total);
        }

        /// <summary>
        /// Método responsável por buscar todas as metas disponíveis no sistema
        /// </summary>
        /// <returns>Lista com as metas disponíveis</returns>
        public IEnumerable<MetasExistentesDTO> BuscarMetasDominio()
        {
            return Mapper.Map<IEnumerable<Metas>, IEnumerable<MetasExistentesDTO>>(_metasService.BuscarMetasDominio());
        }

        /// <summary>
        /// Método responsável por adicionar uma meta
        /// </summary>
        /// <param name="metasDto">Entidade MetasPorLivrosDTO</param>
        public MetasLivrosDTO Add(MetasLivrosDTO metasDto)
        {
            var metas = Mapper.Map<MetasLivrosDTO, MetasPorLivros>(metasDto);
            var metaDominio = _metasService.Add(metas);

            var metaDto = Mapper.Map(metaDominio, metasDto);

            return metaDto;
        }

        /// <summary>
        /// Método responsável por retornar informações de já metas cadastradas por identificador
        /// </summary>
        /// <param name="id">Identificador da meta</param>
        /// <returns>Meta</returns>
        public MetasLivrosDTO GetById(Guid id)
        {
            return Mapper.Map<MetasPorLivros, MetasLivrosDTO>(_metasService.GetById(id));
        }

        /// <summary>
        /// Método responsável por atualizar informações da meta
        /// </summary>
        /// <param name="metasDto">DTO da Meta</param>
        public MetasLivrosDTO Update(MetasLivrosDTO metasDto)
        {
            var metas = Mapper.Map<MetasLivrosDTO, MetasPorLivros>(metasDto);
            var metaSemAtualizar = _metasService.GetById(metas.MetaId);

            metaSemAtualizar.MetaCumprida = metas.MetaCumprida;
            metaSemAtualizar.PaginasDiarias = metas.PaginasDiarias;
            metaSemAtualizar.CapitulosDiarios = metas.CapitulosDiarios;
            metaSemAtualizar.PaginasOuCapitulosLidos = metas.PaginasOuCapitulosLidos;
            metaSemAtualizar.Descricao = metas.Descricao;

            var metaAtualizada = _metasService.Update(metaSemAtualizar);

            return Mapper.Map<MetasPorLivros, MetasLivrosDTO>(metaAtualizada);
        }

        /// <summary>
        /// Método responsável por deletar uma meta
        /// </summary>
        /// <param name="metasDto"></param>
        public void Delete(Guid metaId)
        {
            _metasService.Delete(metaId);
        }

        /// <summary>
        /// Método responsável por limpar o contexto da memória
        /// </summary>
        public void Dispose()
        {
            _metasService.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método responsável por converter uma lista do domínio para uma lista de Dto e aplicar a paginação
        /// </summary>
        /// <param name="listaDominio">Lista de metas do domínio</param>
        /// <param name="total">Quantidade de itens na lista</param>
        /// <returns></returns>
        private FiltrosPesquisaDTO<MetasLivrosDTO> ConverterListaDominioToDto(IEnumerable<MetasPorLivros> listaDominio, int total)
        {
            IEnumerable<MetasLivrosDTO> metas = Mapper.Map<IEnumerable<MetasPorLivros>, IEnumerable<MetasLivrosDTO>>(listaDominio);

            var retornoFiltro = new FiltrosPesquisaDTO<MetasLivrosDTO>
            {
                Total = total
            };

            foreach (var item in metas)
            {
                retornoFiltro.Dto.Add(item);
            }

            return retornoFiltro;
        }
    }
}
