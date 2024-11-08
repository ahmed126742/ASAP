using ASAP.Domain.Entities;
using ASAP.Domain.Repositories.Common;

namespace ASAP.Application.Repositories
{
    public interface IAttachmentRepository : IBaseEntityRepository<Attachment>
    {
        public Task<Attachment> Create(Attachment request, CancellationToken cancellationToken);
        public Task<List<Attachment>> GetAllByHeaderId(Guid headerId, CancellationToken cancellationToken);
        public Task<List<Attachment>> GetAllByHeaderId(List<Guid?> headerIds, CancellationToken cancellationToken);
    }
}
