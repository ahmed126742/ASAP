using ASAP.Application.Repositories;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using CarMaintenance.Application.Features.Attachments.AddAttachment;
using MediatR;

namespace ASAP.Application.Features.Attachments.AddAttachment
{

    public sealed class AddAttachmentHandler : IRequestHandler<AddAttachmentRequest, AddAttachmentResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddAttachmentHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IAttachmentRepository attachmentRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _attachmentRepository = attachmentRepository;
            _mapper = mapper;
        }

        public async Task<AddAttachmentResponse> Handle(AddAttachmentRequest request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.Get(request.UserId, cancellationToken);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var attachment = _mapper.Map<Attachment>(request);
            attachment.CreatedBy = user.FirstName +" "+ user.LastName;

            var entity = await _attachmentRepository.Create(attachment, cancellationToken);

            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<AddAttachmentResponse>(entity);
        }
    }
}
