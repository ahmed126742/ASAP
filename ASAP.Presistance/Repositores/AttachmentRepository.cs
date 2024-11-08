using ASAP.Application.Repositories;
using ASAP.Domain.Entities;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Persistence.Repositories
{
    internal class AttachmentRepository : BaseEntityRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(DataContext context) : base(context)
        {

        }

        public async Task<Attachment> Create(Attachment request, CancellationToken cancellationToken)
        {
            if (request.AttachmentHeaderId == null || request.AttachmentHeaderId == Guid.Empty)
            {
                AttachmentHeader header = new AttachmentHeader();
                _context.AttachmentHeaders.Add(header);
                request.AttachmentHeaderId = header.Id;
            }
            _context.Add(request);
            return request;
        }

        public async Task<List<Attachment>> GetAllByHeaderId(Guid headerId, CancellationToken cancellationToken)
        {
            return await _context.Attachments.Where(a => a.AttachmentHeaderId == headerId && a.Deleted != true).ToListAsync(cancellationToken);
        }

        public async Task<List<Attachment>> GetAllByHeaderId(List<Guid?> headerIds, CancellationToken cancellationToken)
        {
            return await _context.Attachments.Where(x => headerIds.Contains(x.AttachmentHeaderId) && x.Deleted == false).ToListAsync(cancellationToken);
        }

        //public async Task<Attachment> Update(Attachment request, CancellationToken cancellationToken)
        //{
        //    Attachment attachment = await base.GetById(request.Id,cancellationToken);
        //    request.AttachmentHeaderId = attachment.AttachmentHeaderId;
        //    Attachment response = base.Update(request);
        //    return response;
        //}

    }
}
