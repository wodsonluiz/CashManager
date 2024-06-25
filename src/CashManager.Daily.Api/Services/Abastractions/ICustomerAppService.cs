using System.Collections.Generic;
using System.Threading.Tasks;
using CashManager.Daily.Api.Models;

namespace CashManager.Daily.Api.Services.Abstractions
{
    public interface ICustomerAppService
    {
        Task CreateCustomer(CustomerTransactionRequest request);
        Task<CustomerTransactionRequest> GetById(string id);
        Task<IEnumerable<CustomerTransactionRequest>> GetAll();
        Task<CustomerTransactionRequest> GetByDocument(string document);
    }
}