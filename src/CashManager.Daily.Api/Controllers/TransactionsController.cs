using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Daily.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionsController: ControllerBase
    {
        public TransactionsController()
        {
            
        }

        [HttpPost]
        [Route("credit")]
        public async Task<IActionResult> Credit()
        {
            return StatusCode(201);
        }

        [HttpPost]
        [Route("debit")]
        public async Task<IActionResult> Debit()
        {
            return StatusCode(201);
        }
    }
}