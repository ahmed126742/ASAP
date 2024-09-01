using MediatR;

namespace ASAP.Application.Common.Models
{
    public class PaginationRequest<TRequest, TResponse> : IRequest<PagedReponse<TResponse>> 
    {
        public TRequest? Filters { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
