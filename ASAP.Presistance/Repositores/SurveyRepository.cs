using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Presistance.Contexts;
using ASAP.Presistance.Repositores.Common;

namespace ASAP.Presistance.Repositores
{
    public class SurveyRepository : BaseEntityRepository<Survey>, ISurveyRepository
    {
        public SurveyRepository(DataContext context) : base(context)
        {
        }
    }
}
