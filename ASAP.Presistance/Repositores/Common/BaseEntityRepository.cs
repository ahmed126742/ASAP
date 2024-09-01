using System.Linq.Expressions;
using ASAP.Domain.Entities.Common;
using ASAP.Domain.Repositories.Common;
using ASAP.Presistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Repositores.Common
{
    public class BaseEntityRepository<T> : IBaseEntityRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;

        public BaseEntityRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<T> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task<IList<T>> GetAll(CancellationToken cancellationToken)
        {
           return await GetAllAsQuarble().ToListAsync(cancellationToken);
        }

        public void SoftDeletion(T entity)
        {
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            Update(entity);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _context.Update(entity);
        }

        protected IQueryable<T> GetAllAsQuarble(Expression<Func<T, bool>> ? filters = null)
        {
            return  _context.Set<T>()
                .Where(filters);
        }

        private IQueryable<T> GeQuarableAsPaginated(int page, int rows, Expression<Func<T, bool>>? filters = null)
        {
            return GetAllAsQuarble(filters)
                .Skip((page - 1) * rows)
                .Take(rows);
        }
    }
}
