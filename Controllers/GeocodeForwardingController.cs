using AspnetCoreBackendExploratory.Services;
using AspnetCoreBackendExploratory.Services.Contracts.PositionAPIForwardRecords;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreBackendExploratory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocodeForwardingController : ControllerBase
    {
        public GeocodeForwardingController(GeoCodeForwardingService geoService)
        {
            this.geoService = geoService;
        }

        private readonly GeoCodeForwardingService geoService;


        [HttpGet]
        public async Task<ForwardData[]> Get(string location, CancellationToken canceltoken)
        {
            return await this.geoService.RetrieveGeocodeForwardData(location, canceltoken);
        }

    }
}
