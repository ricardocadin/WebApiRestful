using System;
using System.Collections.Generic;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Domain.Interfaces.Repository
{
    public interface ILivrosRepository : IRepositoryBase<Livros>
    {
        IEnumerable<Livros> PesquisarLivros(string tituloLivro, Guid? metaId, Guid? categoriaId, out int total, int skip = 0, int take = 10);
        IEnumerable<Livros> BuscarPorTitulo(string tituloLivro, out int total, int skip = 0, int take = 10);
        IEnumerable<Livros> LivrosPorMeta(Guid? metaId, out int total, int skip = 0, int take = 10);
        IEnumerable<Livros> LivrosPorCategoria(Guid? categoriaId, out int total, int skip = 0, int take = 10);
    }
}
