using System;
using System.Collections.Generic;
using System.Linq;
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
    public class LivrosApplication : ILivrosApplication
    {
        private readonly ILivrosService _livrosService;

        public LivrosApplication(ILivrosService livrosService)
        {
            _livrosService = livrosService;
        }

        /// <summary>
        /// Retorna o livro de acordo com seu identificador
        /// </summary>
        /// <param name="livroId">Identificador do livro</param>
        /// <returns></returns>
        public LivrosDTO GetById(Guid livroId)
        {
            return Mapper.Map<Livros, LivrosDTO>(_livrosService.GetById(livroId));
        }

        public FiltrosPesquisaDTO<LivrosDTO> PesquisarLivros(PesquisaDTO pesquisaDto, int skip = 0, int take = 10)
        {
            var total = 0;

            var listaLivros = _livrosService.PesquisarLivros(pesquisaDto.Titulo, pesquisaDto.MetaDominioId, 
                pesquisaDto.CategoriaId, pesquisaDto.UsuarioId, out total, skip, take);

            return ConverterListaDominioToDto(listaLivros, total);
        }

        /// <summary>
        /// Método responsável por buscar os livros de acordo com o título recebido
        /// </summary>
        /// <param name="livrosDto">DTO da entidade Livro</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de resultados filtrados</returns>
        public FiltrosPesquisaDTO<LivrosDTO> BuscarPorTitulo(LivrosDTO livrosDto, int skip = 0, int take = 10)
        {
            var total = 0;

            var livrosDominio = Mapper.Map<LivrosDTO, Livros>(livrosDto);
            var listaLivros = _livrosService.LivrosPorCategoria(livrosDominio, out total, skip, take);

            return ConverterListaDominioToDto(listaLivros, total);

        }

        /// <summary>
        /// Método responsável por buscar os livros de acordo com uma meta
        /// </summary>
        /// <param name="livrosDto">DTO da entidade Livro</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public FiltrosPesquisaDTO<LivrosDTO> LivrosPorMeta(PesquisaDTO pesquisaDto, int skip = 0, int take = 10)
        {
            var total = 0;

            var listaLivros = _livrosService.LivrosPorMeta(pesquisaDto.MetaDominioId, pesquisaDto.UsuarioId,
                out total, skip, take);

            return ConverterListaDominioToDto(listaLivros, total);
        }

        /// <summary>
        /// Método responsável por buscar os livros de acordo com uma categoria
        /// </summary>
        /// <param name="livrosDto">DTO da entidade Livro</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public FiltrosPesquisaDTO<LivrosDTO> LivrosPorCategoria(LivrosDTO livrosDto, int skip = 0, int take = 10)
        {
            var total = 0;

            var livrosDomain = Mapper.Map<LivrosDTO, Livros>(livrosDto);
            var listaLivros = _livrosService.LivrosPorCategoria(livrosDomain, out total, skip, take);

            return ConverterListaDominioToDto(listaLivros, total);
        }

        /// <summary>
        /// Método responsável por buscar todos os livros
        /// </summary>
        /// <param name="livrosDto">DTO da entidade Livro</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public FiltrosPesquisaDTO<LivrosDTO> GetAll(LivrosDTO livrosDto, int skip, int take)
        {
            var total = 0;

            var livrosDomain = Mapper.Map<LivrosDTO, Livros>(livrosDto);
            var listaLivros = _livrosService.GetAll(livrosDomain, out total, skip, take);

            return ConverterListaDominioToDto(listaLivros, total);
        }

        /// <summary>
        /// Método responsável por adicionar um livro ao domínio
        /// </summary>
        /// <param name="livrosDto">Livro a ser adicionado</param>
        public LivrosDTO Add(LivrosDTO livrosDto)
        {
            var livros = Mapper.Map<LivrosDTO, Livros>(livrosDto);
            
            var livroDomain = _livrosService.Add(livros);
            var livroDto = Mapper.Map(livroDomain, livrosDto);

            return livroDto;
        }

        /// <summary>
        /// Responsável por deletar um livro do domínio
        /// </summary>
        /// <param name="livroDto">Entidade Livro</param>
        public void Delete(Guid livroId)
        {
            _livrosService.Delete(livroId);
        }

        /// <summary>
        /// Responsável por limpar o contexto da aplicação
        /// </summary>
        public void Dispose()
        {
            _livrosService.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método responsável por converter uma lista do domínio para uma lista de Dto e aplicar a paginação
        /// </summary>
        /// <param name="listaDominio">Lista de livros do domínio</param>
        /// <param name="total">Quantidade de itens na lista</param>
        /// <returns></returns>
        private FiltrosPesquisaDTO<LivrosDTO> ConverterListaDominioToDto(IEnumerable<Livros> listaDominio, int total)
        {
            IEnumerable<LivrosDTO> livros = Mapper.Map<IEnumerable<Livros>, IEnumerable<LivrosDTO>>(listaDominio);

            var retornoFiltro = new FiltrosPesquisaDTO<LivrosDTO>
            {
                Total = total
            };

            foreach (var item in livros)
            {
                retornoFiltro.Dto.Add(item);
            }

            return retornoFiltro;
        }
    }
}
