using AutoMapper;
using ASAP.Application.Repositories;
using ASAP.Domain.Entities;
using MediatR;
using ASAP.Domain.Repositories.Common;
using CarMaintenance.Application.Features.Attachments.GetAttachmentsByHeaderId;

namespace ASAP.Application.Features.Attachments.GetAttachmentsByHeaderId
{

    public sealed class GetAttachmentsByHeaderIdHandler : IRequestHandler<GetAttachmentsByHeaderIdRequest, List<GetAttachmentsByHeaderIdResponse>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IMapper _mapper;

        public GetAttachmentsByHeaderIdHandler(IUnitOfWork unitOfWork, IAttachmentRepository attachmentRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAttachmentsByHeaderIdResponse>> Handle(GetAttachmentsByHeaderIdRequest request, CancellationToken cancellationToken)
        {
            //Attachment attachment = _mapper.Map<Attachment>(request);
            List<Attachment> returnedAttachment = await _attachmentRepository.GetAllByHeaderId(request.AttachmentHeaderId, cancellationToken);
            return _mapper.Map<List<GetAttachmentsByHeaderIdResponse>>(returnedAttachment);
        }
    }
}
