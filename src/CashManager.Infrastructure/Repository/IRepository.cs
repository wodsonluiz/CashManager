using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CashManager.Infrastructure.Repository
{
    public interface IRepository<BsonDocument>
    {
        Task AddAsync(BsonDocument entity, CancellationToken cancellationToken = default);
        Task<BsonDocument> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BsonDocument>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<BsonDocument>> GetByFilterAsync(Expression<Func<BsonDocument, bool>> filter, CancellationToken cancellationToken = default);
    }
}