using AutoMapper;
using Multiple.Extensions;
using Multiple.Models;
using Multiple.Models.Abstractions;
using Multiple.Repositories.Abstractions;
using Multiple.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Multiple.Services
{
    public class BaseService<TViewModel, TDatabaseModel> : IBaseService<TViewModel, TDatabaseModel>
        where TViewModel : IViewModel
        where TDatabaseModel : BaseEntity
    {
        protected readonly IRepository<TDatabaseModel> _repository;
        protected readonly IMapper _mapper;
        private IRepository<TDatabaseModel> repository;
        private IMapper mapper;

        public BaseService(IRepository<TDatabaseModel> repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public virtual ServiceResponse<bool> Delete(int id)
        {
            var response = new ServiceResponse<bool>(null);
            //TDatabaseModel databaseModel = this._mapper.Map<TDatabaseModel>(/*model*/);
            PropertyInfo property = typeof(TDatabaseModel).GetProperties().Where(prop => prop.GetCustomAttribute(typeof(KeyAttribute)) != null).FirstOrDefault();
            var query = from data in this._repository.Table select data;
            TDatabaseModel databaseModel = query.Where(field: property, value: id).FirstOrDefault();
            if (databaseModel == null)
            {
                response.Count = 0;
                response.IsSuccessful = false;
                response.ExceptionMessage = "No Records to Delete";
                response.HasExceptionError = true;
                return response;
            }

            int count = Convert.ToInt32(this._repository.Delete(databaseModel));
            response.Count = count;
            response.Entity = count > 0;
            response.IsSuccessful = true;
            return response;
        }
        public virtual ServiceResponse<bool> Delete(int id, bool hardDelete)
        {
            var response = new ServiceResponse<bool>(null);
            PropertyInfo property = typeof(TDatabaseModel).GetProperties().Where(prop => prop.GetCustomAttribute(typeof(KeyAttribute)) != null).FirstOrDefault();
            var query = from data in this._repository.Table select data;
            TDatabaseModel databaseModel = query.Where(field: property, value: id).FirstOrDefault();
            if (databaseModel == null)
            {
                response.Count = 0;
                response.IsSuccessful = false;
                response.ExceptionMessage = "No Records to Delete";
                response.HasExceptionError = true;
                return response;
            }
            int count = 0;
            if (!hardDelete)
            {
                count = Convert.ToInt32(this._repository.Delete(databaseModel));
            }
            else
            {
                count = Convert.ToInt32(this._repository.Delete(databaseModel, true));
            }

            response.Count = count;
            response.Entity = count > 0;
            response.IsSuccessful = true;
            return response;
        }

        public virtual ServiceResponse<long> Delete(IEnumerable<int> idList)
        {
            var response = new ServiceResponse<long>(null);
            //IEnumerable<TDatabaseModel> databaseModels = this._mapper.Map<IEnumerable<int>>(entities);
            //int count = this._repository.Delete(databaseModels);
            response.Count = 0;
            response.Entity = 0;
            response.HasExceptionError = true;
            response.ExceptionMessage = "Service not available";
            response.IsSuccessful = false;
            return response;
        }

        public virtual ServiceResponse<TViewModel> GetById(int id)
        {
            var response = new ServiceResponse<TViewModel>(null);
            PropertyInfo property = typeof(TDatabaseModel).GetProperties().Where(prop => prop.GetCustomAttribute(typeof(KeyAttribute)) != null).FirstOrDefault();
            var query = from data in this._repository.TableNoTracking select data;
            TDatabaseModel model = query.Where(field: property, value: id).FirstOrDefault();

            response.Entity = this._mapper.Map<TViewModel>(model);
            response.IsSuccessful = true;
            return response;
        }

        public ServiceResponse<TViewModel> GetById(int id, params Expression<Func<TDatabaseModel, object>>[] includes)
        {
            var response = new ServiceResponse<TViewModel>(null);
            PropertyInfo property = typeof(TDatabaseModel).GetProperties().Where(prop => prop.GetCustomAttribute<KeyAttribute>(true) != null).FirstOrDefault();
            var query = from data in this._repository.TableNoTracking select data;
            IEnumerable<TDatabaseModel> models = query.Where(field: property, value: id).IncludeMultiple(includes);
            TDatabaseModel model = models.FirstOrDefault();
            response.Entity = this._mapper.Map<TViewModel>(model);
            response.List = this._mapper.Map<IList<TViewModel>>(models);
            if (response.List != null)
            {
                response.Count = response.List.Count;
            }
            response.IsSuccessful = true;
            return response;
        }
        public virtual ServiceResponse<long> Insert<TInsertViewModel>(TInsertViewModel model) where TInsertViewModel : IViewModel
        {
            var response = new ServiceResponse<long>(null);
            TDatabaseModel databaseModel = this._mapper.Map<TDatabaseModel>(model);
            long id = this._repository.Insert(databaseModel);
            response.Count = 1;
            response.Entity = id;
            response.IsSuccessful = true;
            return response;
        }

        public virtual ServiceResponse<long> Insert<TInsertViewModel>(IEnumerable<TInsertViewModel> entities) where TInsertViewModel : IViewModel
        {
            var response = new ServiceResponse<long>(null);
            IEnumerable<TDatabaseModel> databaseModels = this._mapper.Map<IEnumerable<TDatabaseModel>>(entities);
            int count = Convert.ToInt32(this._repository.Insert(databaseModels));
            response.Count = count;
            response.Entity = count;
            response.IsSuccessful = true;
            return response;
        }

        public virtual ServiceResponse<TViewModel> List(int rowCount)
        {
            var response = new ServiceResponse<TViewModel>(null);
            var query = this._repository.Table.Take(rowCount);
            response.Count = query.Count();

            var entities = query.ToList();
            IEnumerable<TViewModel> viewModels = this._mapper.Map<IEnumerable<TViewModel>>(entities);
            response.List = viewModels.ToList();
            response.IsSuccessful = true;

            return response;
        }

        public virtual ServiceResponse<int> GetCount()
        {
            var response = new ServiceResponse<int>(null);
            var query = this._repository.Table;
            response.Count = query.Count();
            response.Entity = response.Count;
            response.IsSuccessful = true;

            return response;
        }

        public virtual ServiceResponse<long> Update<TInsertViewModel>(TInsertViewModel model) where TInsertViewModel : IViewModel
        {
            var response = new ServiceResponse<long>(null);
            TDatabaseModel databaseModel = this._mapper.Map<TDatabaseModel>(model);
            long updatedId = this._repository.Update(databaseModel);

            response.Count = 1;
            response.Entity = updatedId;
            response.IsSuccessful = true;
            return response;
        }

        public virtual ServiceResponse<long> Update<TInsertViewModel>(IEnumerable<TInsertViewModel> entities) where TInsertViewModel : IViewModel
        {
            var response = new ServiceResponse<long>(null);
            IEnumerable<TDatabaseModel> databaseModels = this._mapper.Map<IEnumerable<TDatabaseModel>>(entities);
            int count = Convert.ToInt32(this._repository.Update(databaseModels));

            response.Count = count;
            response.Entity = count;
            response.IsSuccessful = true;
            return response;
        }
    }
}