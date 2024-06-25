using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CashManager.Infrastructure.Repository
{
    public interface IRepository<BsonDocument>
    {
        Task Add(BsonDocument entity);
        Task<BsonDocument> GetById(string id);
        Task<IEnumerable<BsonDocument>> GetAll();
        Task<IEnumerable<BsonDocument>> GetByFilter(Expression<Func<BsonDocument, bool>> filter);
        Task Update(BsonDocument entity);
        Task Delete(string id);
    }
}