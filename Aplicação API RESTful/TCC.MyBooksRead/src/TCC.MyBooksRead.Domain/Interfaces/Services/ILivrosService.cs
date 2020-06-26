using System;
using System.Collections.Generic;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Domain.Interfaces.Services
{
    /// <summary>
    ///     Interface de contratado para os serviços disponíveis para comunicação com o domínio
    /// </summary>
    public interface ILivrosService : IDisposable
    {
        IEnumerable<Livros> PesquisarLivros(string titulo, Guid? metaDominioId, Guid? categoriaId, Guid usuarioId, out int total, int skip, int take);
        IEnumerable<Livros> BuscarPorTitulo(Livros livro, out int total, int skip, int take);
        IEnumerable<Livros> LivrosPorMeta(Guid? metaDominioId, Guid usuarioId, out int total, int skip, int take);
        IEnumerable<Livros> LivrosPorCategoria(Livros livro, out int total, int skip, int take);
        IEnumerable<Livros> GetAll(Livros livro, out int total, int skip, int take);
        Livros GetById(Guid id);
        Livros Add(Livros livro);
        void Delete(Guid id);
    }
}
