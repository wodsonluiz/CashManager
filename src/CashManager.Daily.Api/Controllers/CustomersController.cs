using System.Threading.Tasks;
using CashManager.Daily.Api.Domain.CustomerAgg;
using CashManager.Daily.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Daily.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/customers")]
    public class CustomersController: ControllerBase
    {
        private readonly ICustomerAppService _service;

        public CustomersController(ICustomerAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer customer)
        {
            await _service.CreateCustomer(customer);

            return Created();
        }
    }
}