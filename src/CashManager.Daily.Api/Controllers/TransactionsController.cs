using System.Threading.Tasks;
using CashManager.Daily.Api.Domain;
using CashManager.Daily.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Daily.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionsController: ControllerBase
    {
        private readonly ITransactionAppService _service;

        public TransactionsController(ITransactionAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("credit")]
        public async Task<IActionResult> Credit([FromBody] Transaction transaction)
        {
            await _service.CreateCredit(transaction);

            return StatusCode(201);
        }

        [HttpPost]
        [Route("debit")]
        public async Task<IActionResult> Debit([FromBody] Transaction transaction)
        {
            await _service.CreateDebit(transaction);
            
            return StatusCode(201);
        }
    }
}