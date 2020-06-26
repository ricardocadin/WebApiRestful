using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;

namespace TCC.MyBooksRead.Infra.Data.Repositories
{
    public class LivrosRepository : RepositoryBase<Livros>, ILivrosRepository
    {
        /// <summary>
        /// Método responsável por buscar os livros de acordo com uma meta, categoria ou titulo
        /// </summary>
        /// <param name="livro">Entidade Livro</param>
        /// <param name="meta">Entidade Meta</param>
        /// <param name="categoria">Entidade Categoria</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns></returns>
        public IEnumerable<Livros> PesquisarLivros(string tituloLivro, Guid? metaId, Guid? categoriaId, out int total, int skip = 0,
            int take = 10)
        {
            IQueryable<Livros> listaCompletaDeLivros = (IQueryable<Livros>) base.GetAll("Metas", "Categorias");

            if (!string.IsNullOrEmpty(tituloLivro))
                listaCompletaDeLivros = listaCompletaDeLivros.Where(x => Equals(x.Titulo.ToUpper(), tituloLivro.ToUpper()));

            if (metaId != Guid.Empty)
                listaCompletaDeLivros = listaCompletaDeLivros.Where(x => x.Metas.Any(m => m.MetaDominioId == metaId));

            if (categoriaId != Guid.Empty)
                listaCompletaDeLivros = listaCompletaDeLivros.Where(x => x.CategoriaId == categoriaId);

            total = listaCompletaDeLivros.Count();
            var livrosRetorno = listaCompletaDeLivros.ToList();
            return livrosRetorno.Skip(skip).Take(take);
        }

        /// <summary>
        /// Obtém livros de acordo com o título informado
        /// </summary>
        /// <param name="tituloLivro">Titulo pesquisado</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public IEnumerable<Livros> BuscarPorTitulo(string tituloLivro, out int total, int skip = 0, int take = 10)
        {
            var livros = base.GetAll();
            livros = livros.Where(x => Equals(x.Titulo.ToUpper(), tituloLivro.ToUpper()));

            total = livros.Count();
            var livrosRetorno = livros.ToList();
            return livrosRetorno.Skip(skip).Take(take);
        }

        /// <summary>
        /// Obtém os livros de acordo com uma meta específica
        /// </summary>
        /// <param name="metaDominioId">Identificador da Meta</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public IEnumerable<Livros> LivrosPorMeta(Guid? metaDominioId, out int total, int skip = 0, int take = 10)
        {
            var livrosComMetas = base.GetAll("Metas");

            livrosComMetas = livrosComMetas.Where(x => x.Metas.Any(m => m.MetaDominioId == metaDominioId)).ToList();
            total = livrosComMetas.Count();
            return livrosComMetas.Skip(skip).Take(take);
        }

        /// <summary>
        /// Obtém os livros de acordo com uma categoria específica
        /// </summary>
        /// <param name="categoriaId">Identificado da categoria</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns>Lista de livros filtrados</returns>
        public IEnumerable<Livros> LivrosPorCategoria(Guid? categoriaId, out int total, int skip = 0, int take = 10)
        {
            var livrosDaCategoria = base.GetAll("Categorias");

            livrosDaCategoria = livrosDaCategoria.Where(x => x.Categorias.CategoriasDominioId == categoriaId).ToList();
            total = livrosDaCategoria.Count();
            return livrosDaCategoria.Skip(skip).Take(take);
        }

        /// <summary>
        /// Método responsável por buscar o livro pelo seu identificador incluindo todos seus relacionamentos
        /// </summary>
        /// <param name="id">Identificador do livro</param>
        /// <returns>Livro filtrado</returns>
        public override Livros GetById(Guid id)
        {
            IQueryable<Livros> livro = _dbContext.Livros
                .Include(x => x.Metas)
                .Include(x => x.Categorias)
                .Include(x => x.Autores)
                .Include(x => x.Usuario);

            return livro.FirstOrDefault(x => x.LivroId == id);
        }
    }
}

