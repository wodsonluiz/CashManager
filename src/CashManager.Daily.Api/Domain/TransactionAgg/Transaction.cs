using System;

namespace CashManager.Daily.Api.Domain
{
    public class Transaction
    {
        public string? Id { get; set; } 
        public DateTime Date { get; set; }  
        public string? TypeOperation { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}