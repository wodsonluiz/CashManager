using System.Collections.Generic;

namespace CashManager.Report.Api.Models
{
    public class ReportContentResponse
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
        public long Balance { get; set; }
        public List<TransactionContentResponse>? Transactions { get; set; }
    }
}