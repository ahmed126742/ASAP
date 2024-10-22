using System.Linq.Expressions;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;

namespace ASAP.Presistance.Repositores
{
    public class SupplierRepository : BaseEntityRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Supplier> GetFilteredSuppliers(string? searchText = "", int? SupplierTypeId = null)
        {
            return GetAllAsQuarble(FilterSuppliers(searchText, SupplierTypeId));
        }

        private Expression<Func<Supplier, bool>> FilterSuppliers(string? searchText = "", int? SupplierTypeId = null)
        {
            return x => (string.IsNullOrEmpty(searchText) || x.Name.ToLower().Contains(searchText.ToLower())) 
            && (SupplierTypeId == null || x.SupplierTypeId == SupplierTypeId );
        }
    }
}
