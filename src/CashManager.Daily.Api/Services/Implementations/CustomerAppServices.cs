using System.Threading.Tasks;
using CashManager.Daily.Api.Domain.CustomerAgg;
using CashManager.Daily.Api.Repository;
using CashManager.Daily.Api.Services.Abstractions;

namespace CashManager.Daily.Api.Services.Implementations
{
    public class CustomerAppServices: ICustomerAppService
    {
        private readonly IRepository<Customer> _repository;

        public CustomerAppServices(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public Task CreateCustomer(Customer customer)
        {
            return _repository.Add(customer);
        }
    }
}