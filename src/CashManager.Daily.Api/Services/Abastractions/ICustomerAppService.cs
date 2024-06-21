using System.Threading.Tasks;
using CashManager.Daily.Api.Domain.CustomerAgg;

namespace CashManager.Daily.Api.Services.Abstractions
{
    public interface ICustomerAppService
    {
        Task CreateCustomer(Customer customer);
    }
}