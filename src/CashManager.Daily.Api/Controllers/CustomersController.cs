using System.Threading.Tasks;
using CashManager.Daily.Api.Models;
using CashManager.Daily.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Daily.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerTransactionsController: ControllerBase
    {
        private readonly ICustomerAppService _service;

        public CustomerTransactionsController(ICustomerAppService service)
        {
            _service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerTransactionRequest customer)
        {
            await _service.CreateCustomer(customer);

            return StatusCode(201);
        }

        

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAll();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            var result = await _service.GetById(id);

            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("{document}/by-document")]
        public async Task<IActionResult> GetByName([FromRoute]string document)
        {
            if(string.IsNullOrEmpty(document))
                return BadRequest($"Documento invalid {document}");
                
            var result = await _service.GetByDocument(document);

            if(result == null)
                return NotFound();

            return Ok(result);
        }
    }
}