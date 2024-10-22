using ASAP.Application.Common;
using ASAP.Application.Common.Models;
using ASAP.Application.Features.Users.GetFilteredUsers;
using ASAP.Application.Services.Supplier;
using ASAP.Application.Services.Supplier.DTOs.Processing;
using ASAP.Application.Services.Supplier.DTOs.Retreival;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        public readonly ISupplierRepository _supplierRepository;
        public readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(
            ISupplierRepository supplierRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetSupplierResponse> GetSupplierAsync(GetSupplierRequest request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.Get(request.Id, cancellationToken);
            if (supplier == null)
                throw new NotFoundException("supplier deos not exist!");

            return _mapper.Map<GetSupplierResponse>(supplier);
        }

        public async Task<PagedReponse<GetFilteredSuppliersResponse>> GetPagedFilteresSuppliers(PaginationRequest<GetFilteredSuppliersRequest, GetFilteredUsersResponse> request, CancellationToken cancellationtoken)
        {
            int? supplierTypeId = request.Filters.SupplierTypeId == null ? null : (int)request.Filters.SupplierTypeId;
            var suppliers = _supplierRepository.GetFilteredSuppliers(request.Filters.SearchText, supplierTypeId);
            var filteredSuppliers = _mapper.Map<List<GetFilteredSuppliersResponse>>(await suppliers.OrderBy(x=>x.Sorted).ToListAsync(cancellationtoken));
            return new PagedReponse<GetFilteredSuppliersResponse>(filteredSuppliers, await suppliers.CountAsync(), request.PageNumber, request.PageSize);
        }

        public async Task<Guid> CreateSupplierAsync(CreateSupplierRequest request, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Domain.Entities.Supplier>(request);
            _supplierRepository.Create(supplier);
            await _unitOfWork.Save(cancellationToken);
            return supplier.Id;
        }

        public async Task UpdateSupplierAsync(UpdateSupplierRequest request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.Get(request.Id, cancellationToken);
            if (supplier == null)
                throw new NotFoundException("SupplierId does not exist!");

            _mapper.Map(request, supplier);
            _supplierRepository.Update(supplier);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task DeleteSupplierAsync(DeleteSupplierRequest request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierRepository.Get(request.Id, cancellationToken);
            if (supplier == null)
                throw new NotFoundException("supplierId deos not exist!");

            _supplierRepository.Delete(supplier);
            await _unitOfWork.Save(cancellationToken);
        }
    }
}
