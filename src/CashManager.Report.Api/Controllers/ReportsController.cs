using System.Threading;
using System.Threading.Tasks;
using CashManager.Report.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Report.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportsController: ControllerBase
    {
        private readonly IReportAppService _service;
        private readonly CancellationTokenSource _cts;

        public ReportsController(IReportAppService service)
        {
            _service = service;
            _cts = new CancellationTokenSource();
        }

        [HttpGet]
        [Route("{document}/by-document")]
        public async Task<IActionResult> ReportByDocument(string document)
        {
            if(string.IsNullOrEmpty(document))
                return BadRequest("Document invalid");

            var result = await _service.GetReportContentByDocumentAsync(document, _cts.Token);

            return Ok(result);
        }
    }
}