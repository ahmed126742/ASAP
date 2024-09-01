using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Domain.Repositories
{
    public interface IUserRepository : IBaseEntityRepository<Client>
    {
        Task<bool> CheckUserExistanceAsync(string email, CancellationToken cancellationToken);

        Task<Client> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

        Task<IList<string>> GetUsersEmailsAsync(CancellationToken cancellationToken);

        IQueryable<Client> GetFilteredUsers(string? searchText = "");
    }
}
