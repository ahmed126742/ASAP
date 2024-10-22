using System.Linq.Expressions;
using ASAP.Domain.Entities.Common;

namespace ASAP.Domain.Repositories.Common
{
    public interface IBaseEntityRepository<T> where T : BaseEntity
    {
        Task<T> Get(Guid id, CancellationToken cancellationToken);
        Task<IList<T>> GetAll(CancellationToken cancellationToken);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SoftDeletion(T entity);

        IQueryable<T> GetAllAsQuarble(Expression<Func<T, bool>>? filters = null);
    }
}
