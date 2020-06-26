using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Infra.Data.Context;

namespace TCC.MyBooksRead.Infra.Data.Repositories
{
    /// <summary>
    /// Implementação das operações genéricas de CRUD usando Repository Pattern
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public MyBooksReadContext _dbContext;
        public DbSet<TEntity> _DbSet;

        /// <summary>
        /// Método responsável por recuperar uma instância do contexto e atribuir ao DbSet a entidade para manipulação dos dados
        /// </summary>
        public RepositoryBase()
        {
            _dbContext = new MyBooksReadContext();
            _DbSet = _dbContext.Set<TEntity>();
        }

        //TODO: Codificar contratos da interface

        /// <summary>
        /// Método responsável por adicionar uma nova entidade no contexto
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Add(TEntity entity)
        {
            _DbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Método responsável por buscar entidade por id
        /// </summary>
        /// <param name="id">Identificador</param>
        public virtual TEntity GetById(Guid id)
        {
            return _DbSet.Find(id);
        }

        /// <summary>
        /// Método responsável por buscar todas as entidades do contexto
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = _DbSet.ToList();
            return query;
        }

        /// <summary>
        /// Método responsável por buscar todas as entidades do contexto
        /// </summary>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        public virtual IEnumerable<TEntity> GetAll(out int total, int skip = 0, int take = 10)
        {
            var query = _DbSet.ToList();
            total = query.Count();
            return query.Skip(skip).Take(take);
        }

        /// <summary>
        /// Método responsável por realizar buscas mais especializadas através de lambdas expressions
        /// </summary>
        /// <param name="predicate">Expressão Lambda</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _DbSet.Where(predicate);
        }

        /// <summary>
        /// Obtém todos os registros utilizando agragação
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll(params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

        /// <summary>
        /// Obtém todas as entidades e todas as classes de referência descritas na propriedade includeProperties
        /// por meio de agragação
        /// </summary>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <param name="includeProperties">Propriedades filhas da entidade</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll(out int total, int skip = 0, int take = 10, params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            total = query.Count();
            return query.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// Método responsável por atualizar a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }


        /// <summary>
        /// Método responsável por realizar a deleção da entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual void Delete(TEntity entity)
        {
            _DbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Método responsável por retirar o contexto da memória
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
