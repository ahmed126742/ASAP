using ASAP.Application.Repositories;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using CarMaintenance.Application.Features.Attachments.DeleteAttachment;
using MediatR;

namespace ASAP.Application.Features.Attachments.DeleteAttachment
{

    public sealed class DeleteAttachmentHandler : IRequestHandler<DeleteAttachmentRequest, DeleteAttachmentResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DeleteAttachmentHandler(IUnitOfWork unitOfWork,
            IAttachmentRepository attachmentRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attachmentRepository = attachmentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<DeleteAttachmentResponse> Handle(DeleteAttachmentRequest request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.Get(request.UserId,cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            Attachment attachment = _mapper.Map<Attachment>(request);
            attachment = await _attachmentRepository.Get(request.AttachmentId, cancellationToken);
            attachment.UpdatedBy = user.FirstName +" "+ user.LastName;
            _attachmentRepository.Delete(attachment);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<DeleteAttachmentResponse>(attachment);
        }
    }
}
