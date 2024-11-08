using AutoMapper;
using ASAP.Application.Repositories;
using MediatR;
using CarMaintenance.Application.Features.AttachmentFeatures.GetAttachmentById;

namespace ASAP.Application.Features.AttachmentFeatures.GetAttachmentById;

public sealed class GetAttachmentByIdHandler : IRequestHandler<GetAttachmentByIdRequest, GetAttachmentByIdResponse>
{
    private readonly IAttachmentRepository _repository;
    private readonly IMapper _mapper;

    public GetAttachmentByIdHandler(IAttachmentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GetAttachmentByIdResponse> Handle(GetAttachmentByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(request.Id, cancellationToken);
        return _mapper.Map<GetAttachmentByIdResponse>(result);
    }
}