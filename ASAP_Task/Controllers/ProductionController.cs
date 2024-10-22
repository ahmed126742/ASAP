using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        public ProductionController()
        {
        }

        [HttpPost("CreateProduction")]
        public async Task CreateProduction()
        {
        }

        [HttpPost("GetProduction")]
        public async Task GetProduction()
        {
        }

        [HttpPost("GetProductions")]
        public async Task GetProductions()
        {
        }

        [HttpPost("UpdateProduction")]
        public async Task UpdateProduction()
        {
        }

        [HttpPost("DeleteProduction")]
        public async Task DeleteProduction()
        {
        }
    }
}
