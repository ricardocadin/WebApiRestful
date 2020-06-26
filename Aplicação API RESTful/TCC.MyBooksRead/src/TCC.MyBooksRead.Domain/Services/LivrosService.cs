using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Interfaces.Services;
using TCC.MyBooksRead.Domain.Validation;

namespace TCC.MyBooksRead.Domain.Services
{
    /// <summary>
    /// Livros Domain Service - responsável por validar a entidade e persistir a mesma na base, 
    ///                         ele que orquestra as regras para tomar uma decisão.
    /// </summary>
    public class LivrosService : ILivrosService
    {
        private readonly ILivrosRepository _livroRepository;

        public LivrosService(ILivrosRepository livrosrepository)
        {
            _livroRepository = livrosrepository;
        }

        /// <summary>
        /// Método responsável por buscar os livros de acordo com uma meta, categoria ou titulo
        /// </summary>
        /// <param name="pesquisaDto">Filtros da pesquisa</param>
        /// <param name="usuarioId">Identificador do usuário</param>
        /// <param name="titulo">Titulo do livro</param>
        /// <param name="metaDominioId">Identificador da metado domínio</param>
        /// <param name="categoriaId">Identificador da categoria do´domínio</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns></returns>
        public IEnumerable<Livros> PesquisarLivros(string titulo, Guid? metaDominioId, Guid? categoriaId, Guid usuarioId, out int total, int skip, int take)
        {
            var listaLivros = _livroRepository.PesquisarLivros(titulo,metaDominioId, categoriaId, out total, skip, take);
            return listaLivros.Where(x => x.UsuarioId == usuarioId);
        }

        /// <summary>
        /// Método responsável por buscar os livros de acordo com o título recebido
        /// </summary>
        /// <param name="livro">Entidade Livro</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de resultados filtrados</returns>
        public IEnumerable<Livros> BuscarPorTitulo(Livros livro, out int total, int skip, int take)
        {
            var listaLivros = _livroRepository.BuscarPorTitulo(livro.Titulo, out total, skip, take);
            return listaLivros.Where(x => x.UsuarioId == livro.UsuarioId);
        }

        /// <summary>
        /// Método responsável por buscar os livros de acordo com uma meta
        /// </summary>
        /// <param name="livro">Entidade Livro</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public IEnumerable<Livros> LivrosPorMeta(Guid? metaDominioId, Guid usuarioId, out int total, int skip, int take)
        {
            var listaLivros = _livroRepository.LivrosPorMeta(metaDominioId, out total, skip, take);
            return listaLivros.Where(x => x.UsuarioId == usuarioId);
        }

        /// <summary>
        /// Método responsável por buscar os livros de acordo com uma categoria
        /// </summary>
        /// <param name="livro">Entidade Livro</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public IEnumerable<Livros> LivrosPorCategoria(Livros livro, out int total, int skip, int take)
        {
            var listaLivros = _livroRepository.LivrosPorCategoria(livro.CategoriaId, out total, skip, take);
            return listaLivros.Where(x => x.UsuarioId == livro.UsuarioId);
        }

        /// <summary>
        /// Método responsável por buscar todos os livros
        /// </summary>
        /// <param name="livro">Entidade Livro</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public IEnumerable<Livros> GetAll(Livros livro, out int total, int skip, int take)
        {
            var listaLivros = _livroRepository.GetAll(out total, skip, take);
            return listaLivros.Where(x => x.UsuarioId == livro.UsuarioId);
        }

        /// <summary>
        /// Responsável por recuperar um livro por identificador
        /// </summary>
        /// <param name="id">Identificador do Livro</param>
        /// <returns></returns>
        public Livros GetById(Guid id)
        {
            return _livroRepository.GetById(id);
        }

        /// <summary>
        /// Responsável por adicionar um livro ao domínio
        /// </summary>
        /// <param name="livro">Entidade Livro</param>
        /// <returns></returns>
        public Livros Add(Livros livro)
        {
            var result = new LivroAptoParaInclusaoValidation(_livroRepository).Validate(livro);

            if (!result.IsValid)
            {
                livro.ValidationResult = result;
                return livro;
            }

            livro.ValidationResult = result;
            _livroRepository.Add(livro);
            return livro;
        }

        /// <summary>
        /// Responsável por deletar um livro do domínio
        /// </summary>
        /// <param name="livro">Entidade Livro</param>
        public void Delete(Guid livroId)
        {
            var livroDomain = _livroRepository.GetById(livroId);
            _livroRepository.Delete(livroDomain);
        }

        /// <summary>
        /// Responsável por limpar o contexto da aplicação
        /// </summary>
        public void Dispose()
        {
            _livroRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
