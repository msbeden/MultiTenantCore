using Multiple.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Multiple.Repositories.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        T GetById(object id);
        long Insert(T entity);
        long Insert(IEnumerable<T> entities);
        long Update(T entity);
        long Update(IEnumerable<T> entities);
        long Delete(T entity);
        long Delete(T entity, bool hardDelete);
        IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetSql(string sql);
    }
}
