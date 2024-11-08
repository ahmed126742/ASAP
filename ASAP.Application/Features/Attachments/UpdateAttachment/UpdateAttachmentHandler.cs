using ASAP.Application.Features.Attachments.UpdateAttachment;
using ASAP.Application.Repositories;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using MediatR;

namespace CarMaintenance.Application.Features.Attachments.UpdateAttachment
{

    public sealed class UpdateAttachmentHandler : IRequestHandler<UpdateAttachmentRequest, UpdateAttachmentResponse>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateAttachmentHandler(
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

        public async Task<UpdateAttachmentResponse> Handle(UpdateAttachmentRequest request, CancellationToken cancellationToken)
        {
            Attachment? entity = null;
            User? user = null;
            var oldPath = "";
            try
            {
                entity = await _attachmentRepository.Get(request.AttachmentId, cancellationToken);
                oldPath = entity.Path;
                user = await _userRepository.Get(request.UserId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }

            try
            {
                
                string? currentUserName = user.FirstName + " " + user.LastName;
                _mapper.Map(request, entity);

                entity.UpdatedBy = currentUserName;

                _attachmentRepository.Update(entity);
                await _unitOfWork.Save(cancellationToken);

                if (File.Exists(oldPath))
                {
                    try
                    {
                        File.Delete(oldPath);
                    }
                    catch (Exception ex)
                    {
                        //Do something
                    }
                }
                return _mapper.Map<UpdateAttachmentResponse>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
