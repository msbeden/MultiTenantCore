using Multiple.Enums;
using Multiple.Extensions;
using Multiple.Models;
using Multiple.Models.Abstractions;
using Multiple.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Multiple.Repositories
{
    public class GeneralRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MultipleDbContext _MultipleDbContext;
        private DbSet<T> _entities;
        protected virtual DbSet<T> Entities => this._entities ??= this._MultipleDbContext.Set<T>();
        public GeneralRepository(MultipleDbContext EpazarwebDbContext)
        {
            this._MultipleDbContext = EpazarwebDbContext;
            this._entities = EpazarwebDbContext.Set<T>();
        }
        public IQueryable<T> Table => this.Entities.Where(x => x.Status != (short)Status.Deleted);

        public IQueryable<T> TableNoTracking => this.Entities.Where(x => x.Status != (short)Status.Deleted).AsNoTracking();

        public long Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            PropertyValues entityData = this._MultipleDbContext.Entry(entity).GetDatabaseValues();
            IProperty primaryKeyProp = entityData.Properties.FirstOrDefault(x => x.IsKey());
            object result = entityData.GetValue<object>(propertyName: primaryKeyProp.Name);
            long response = Convert.ToInt64(result);

            entity.Status = -1;

            this._MultipleDbContext.Update(entity);
            this._MultipleDbContext.SaveChanges();

            return response;
        }
        public long Delete(T entity, bool hardDelete)
        {
            if (!hardDelete)
            {
                return this.Delete(entity);
            }

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Entities.Remove(entity);
            long result = this._MultipleDbContext.SaveChanges();
            return result;
        }

        public T GetById(object id)
        {
            T data = this.Entities.Find(id);
            return data.Status == 0 ? null : data;
        }

        public IEnumerable<T> GetSql(string sql)
        {
            return this.Entities.FromSqlRaw(sql);
        }

        public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes)
        {
            return this.TableNoTracking.IncludeMultiple(includes);
        }

        public long Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this._entities.Add(entity);
            var a = this._MultipleDbContext.Entry(entity).State;
            this._MultipleDbContext.SaveChanges();

            PropertyValues entityData = this._MultipleDbContext.Entry(entity).GetDatabaseValues();
            IProperty primaryKeyProp = entityData.Properties.FirstOrDefault(x => x.IsKey());
            object result = entityData.GetValue<object>(propertyName: primaryKeyProp.Name);
            long response = Convert.ToInt64(result);
            return response;
        }

        public virtual long Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                this.Entities.Add(entity);
            }

            long result = this._MultipleDbContext.SaveChanges();
            return result;
        }

        public long Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            this._MultipleDbContext.Update(entity);
            this._MultipleDbContext.SaveChanges();
            this._MultipleDbContext.Entry(entity).State = EntityState.Detached;
            PropertyValues entityData = this._MultipleDbContext.Entry(entity).GetDatabaseValues();
            IProperty primaryKeyProp = entityData.Properties.FirstOrDefault(x => x.IsKey());
            object result = entityData.GetValue<object>(propertyName: primaryKeyProp.Name);
            long response = Convert.ToInt64(result);
            return response;
        }

        public virtual long Update(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            long result = this._MultipleDbContext.SaveChanges();
            return result;
        }
    }

}
