using ASAP.Application.Services;
using ASAP.Application.Services.Stock.DTOs;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;

namespace ASAP.Infrastructure.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public StockService(
            IStockRepository stockRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddStocksAsync(IList<StockRequestDto> request, CancellationToken cancellationToken)
        {
            var stocks =  _mapper.Map<List<Domain.Entities.Stock>>(request);
            await _stockRepository.AddStocksAsync(stocks, cancellationToken);
            try
            {
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
