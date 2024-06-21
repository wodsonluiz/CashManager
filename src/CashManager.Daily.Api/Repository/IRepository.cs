using System.Threading.Tasks;

namespace CashManager.Daily.Api.Repository
{
    public interface IRepository<in T> where T : class
    {
        Task Add(T entity);
    }
}