using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CashManager.Domain.ReportAgg;
using CashManager.Infrastructure.Repository;
using CashManager.Report.Api.Models;
using CashManager.Report.Api.Services.Abstractions;
using MongoDB.Driver;
using Serilog;

namespace CashManager.Report.Api.Services.Implementations
{
    public class ReportAppService: IReportAppService
    {
        private readonly IRepository<ReportDaily> _repository;
        private readonly ILogger _logger;

        public ReportAppService(IRepository<ReportDaily> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ReportContentResponse> GetReportContentByDocumentAsync(string document, CancellationToken cancellationToken = default)
        {
            ReportContentResponse report = null;
            
            try
            {
                var reportsCredit = await _repository.GetByFilterAsync(GetFilter(document, "credit"), cancellationToken);
                var reportsDebit = await _repository.GetByFilterAsync(GetFilter(document, "debit"), cancellationToken);

                report = BuildReport(reportsCredit, reportsDebit);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error in {nameof(GetReportContentByDocumentAsync)}");
            }
            
            return report;
        }

        private ReportContentResponse BuildReport(IEnumerable<ReportDaily> reportsCredit, IEnumerable<ReportDaily> reportsDebit)
        {
            var reportResponse = new ReportContentResponse();

            if (reportsCredit == null && reportsDebit == null)
                return reportResponse;
            
            var customerDetails = reportsCredit?.FirstOrDefault() ?? reportsDebit?.FirstOrDefault();

            long debited = reportsDebit?.Sum(r => r.Transaction.Amount) ?? 0;
            long credited = reportsCredit?.Sum(r => r.Transaction.Amount) ?? 0;
            
            reportResponse.Name = customerDetails?.Name;
            reportResponse.Document = customerDetails?.Document;
            reportResponse.Balance = credited - debited;
            reportResponse.Transactions = [];

            var allReports = reportsCredit?.Concat(reportsDebit) ?? Enumerable.Empty<ReportDaily>();

            foreach (var transaction in allReports.Select(r => r.Transaction))
            {
                reportResponse.Transactions.Add(new TransactionContentResponse
                {
                    Amount = transaction.Amount,
                    DateOperation = transaction.DateOperation,
                    OperationType = transaction.OperationType
                });
            }

            return reportResponse;
        }
    
        private Expression<Func<ReportDaily, bool>> GetFilter(string document, string operationType)
        {
            Expression<Func<ReportDaily, bool>> filter = report => report.Document == document 
                && report.Transaction.OperationType.Equals(operationType, StringComparison.CurrentCultureIgnoreCase);

            return filter;
        }
    }
}