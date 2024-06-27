using System.Threading;
using System.Threading.Tasks;
using CashManager.Report.Api.Models;

namespace CashManager.Report.Api.Services.Abstractions
{
    public interface IReportAppService
    {
        Task<ReportContentResponse> GetReportContentByDocumentAsync(string document, CancellationToken cancellationToken = default);
    }
}