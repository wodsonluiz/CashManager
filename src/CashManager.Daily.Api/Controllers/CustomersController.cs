using System.Threading;
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
        private readonly CancellationTokenSource _cts;

        public CustomerTransactionsController(ICustomerAppService service)
        {
            _service = service;
            _cts = new CancellationTokenSource();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerTransactionRequest customer)
        {
            await _service.CreateCustomerAsync(customer, _cts.Token);

            return StatusCode(201);
        }

        

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync(_cts.Token);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute]string id)
        {
            var result = await _service.GetByIdAsync(id, _cts.Token);

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
                
            var result = await _service.GetByDocumentAsync(document, _cts.Token);

            if(result == null)
                return NotFound();

            return Ok(result);
        }
    }
}