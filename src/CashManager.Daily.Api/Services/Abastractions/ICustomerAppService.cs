using System.Collections.Generic;
using System.Threading.Tasks;
using CashManager.Daily.Api.Domain.CustomerAgg;

namespace CashManager.Daily.Api.Services.Abstractions
{
    public interface ICustomerAppService
    {
        Task CreateCustomer(Customer customer);
        Task DeleteCustomer(string id);
        Task<Customer> GetById(string id);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByDocument(string document);
    }
}