using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CashManager.Daily.Api.Repository
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filter);
        Task Update(T entity);
        Task Delete(string id);
    }
}