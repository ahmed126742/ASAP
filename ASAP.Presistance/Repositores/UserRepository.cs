using System.Linq.Expressions;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Presistance.Repositores
{
    public class UserRepository : BaseEntityRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> CheckUserExistanceAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Set<User>().AnyAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<IList<string>> GetUsersEmailsAsync(CancellationToken cancellationToken)
        {
            return await GetAllAsQuarble().Select(x => x.Email).ToListAsync(cancellationToken);
        }

        public IQueryable<User> GetFilteredUsers(string? searchText = "")
        {
            return GetAllAsQuarble(FitlerUsers(searchText));        
        }

        public Expression<Func<User, bool>> FitlerUsers(string? searchText = "")
        {
            return x => string.IsNullOrEmpty(searchText) ||
            x.FirstName.ToLower().Contains(searchText.ToLower()) ||
            x.LastName.ToLower().Contains(searchText.ToLower()) ||
            x.Email.ToLower().Contains(searchText.ToLower()) ||
            x.PhoneNumber.ToLower().Contains(searchText.ToLower());
        }
    }
}
