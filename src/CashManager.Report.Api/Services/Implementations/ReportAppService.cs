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

namespace CashManager.Report.Api.Services.Implementations
{
    public class ReportAppService: IReportAppService
    {
        private readonly IRepository<ReportDaily> _repository;

        public ReportAppService(IRepository<ReportDaily> repository)
        {
            _repository = repository;
        }

        public async Task<ReportContentResponse> GetReportContentByDocumentAsync(string document, CancellationToken cancellationToken = default)
        {
            var reportResponse = new ReportContentResponse();

            Expression<Func<ReportDaily, bool>> filter = report => report.Document == document;

            var reports = await _repository.GetByFilterAsync(filter, cancellationToken);

            if(reports == null || !reports.Any())
                return reportResponse;

            return BuildReport(reports);
        }

        private ReportContentResponse BuildReport(IEnumerable<ReportDaily> reports)
        {
            var reportResponse = new ReportContentResponse();
            var debitedTransactions = reports.Where(r => r.Transaction.OperationType?.ToLower() == "debit");
            var creditedTransactions = reports.Where(r => r.Transaction.OperationType?.ToLower() == "credit");

            var customerDetails = reports.FirstOrDefault();
            var debited = GetAmount(debitedTransactions);
            var credited = GetAmount(creditedTransactions);
            
            reportResponse.Name = customerDetails?.Name;
            reportResponse.Document = customerDetails?.Document;
            reportResponse.Balance = credited - debited;
            reportResponse.Transactions = new List<TransactionContentResponse>();

            foreach (var report in reports)
            {
                var transaction = new TransactionContentResponse
                {
                    Amount = report.Transaction.Amount,
                    DateOperation = report.Transaction.DateOperation,
                    OperationType = report.Transaction.OperationType
                };

                reportResponse.Transactions!.Add(transaction);
            }

            return reportResponse;
        }

        private long GetAmount(IEnumerable<ReportDaily> reports)
        {
            long amount = 0;

            if(reports == null || !reports.Any())
                return amount;

            amount = reports.Sum(r => r.Transaction.Amount);

            return amount;
        }
    }
}