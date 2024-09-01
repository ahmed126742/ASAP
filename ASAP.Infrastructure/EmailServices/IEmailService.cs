namespace ASAP.Infrastructure.EmailServices
{
    public interface IEmailService
    {

        Task NotifyClientWithUpdates(IEnumerable<string> emails, CancellationToken cancellationToken);
    }
}
