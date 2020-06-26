using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface responsável por distribuir o contrato dos métodos básicos para o repositório
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity entity);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(params string[] includeProperties);
        IEnumerable<TEntity> GetAll(out int total, int skip, int take);
        IEnumerable<TEntity> GetAll(out int total, int skip, int take, params string[] includeProperties);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
