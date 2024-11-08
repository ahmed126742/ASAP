using System.Linq.Expressions;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;

namespace ASAP.Presistance.Repositores
{
    public class ContractorRepository : BaseEntityRepository<Contractor>, IContractorRepository
    {
        public ContractorRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Contractor> GetFilteredContractors(int page, int rows, string? searchText = "")
        {
            return GetQuarableAsPaginated(page, rows, FilterContractors(searchText));
        }

        private Expression<Func<Contractor, bool>> FilterContractors(string? searchText = "")
        {
            return x => (string.IsNullOrEmpty(searchText) || x.Name.ToLower().Contains(searchText.ToLower()));
        }
    }
}
