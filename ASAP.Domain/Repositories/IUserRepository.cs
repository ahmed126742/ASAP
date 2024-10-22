using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Domain.Repositories
{
    public interface IUserRepository : IBaseEntityRepository<User>
    {
        Task<bool> CheckUserExistanceAsync(string email, CancellationToken cancellationToken);

        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<IList<string>> GetUsersEmailsAsync(CancellationToken cancellationToken);

        IQueryable<User> GetFilteredUsers(string? searchText = "");
    }
}
