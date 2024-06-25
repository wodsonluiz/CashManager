using System.Threading.Tasks;
using CashManager.Domain.TransactionAgg;

namespace CashManager.Daily.Api.Services.Abstractions
{
    public interface ITransactionAppService
    {
        /// <summary>
        /// Create transaction to debit in bankaccount customer
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task CreateDebit(Transaction transaction);

        /// <summary>
        /// Create transaction to credit in bankaccount customer
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task CreateCredit(Transaction transaction);
    }
}