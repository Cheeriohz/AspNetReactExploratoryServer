using AspnetCoreBackendExploratory.Services;
using AspnetCoreBackendExploratory.Services.Contracts.PositionAPIForwardRecords;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreBackendExploratory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureForLocationController : ControllerBase
    {
        public TemperatureForLocationController(GeoCodeForwardingService geoService)
        {
            this.geoService = geoService;
        }

        private readonly GeoCodeForwardingService geoService;


        [HttpGet]
        public async Task<object> Get(double lat, double lon)
        {
            return await Task.FromResult(new { degrees = 10 });
        }

    }
}
