using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CashManager.Daily.Api.Models;

namespace CashManager.Daily.Api.Services.Abstractions
{
    public interface ICustomerAppService
    {
        Task CreateCustomerAsync(CustomerTransactionRequest request, CancellationToken cancellationToken = default);
        Task<CustomerTransactionRequest> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CustomerTransactionRequest>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CustomerTransactionRequest> GetByDocumentAsync(string document, CancellationToken cancellationToken = default);
    }
}