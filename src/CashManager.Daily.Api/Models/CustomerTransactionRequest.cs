namespace CashManager.Daily.Api.Models
{
    public class CustomerTransactionRequest
    {
        public string Id { get; set; }

        public string Document { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Profile { get; set; }
        
        public CompanyRequest Company { get; set; }

        public TransactionRequest Transaction { get; set; }
    }    
}