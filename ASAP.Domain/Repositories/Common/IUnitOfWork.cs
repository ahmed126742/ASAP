namespace ASAP.Domain.Repositories.Common
{
    public interface IUnitOfWork
    {
        Task Save(CancellationToken cancellationToken);
    }
}
