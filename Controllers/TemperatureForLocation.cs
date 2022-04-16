using AspnetCoreBackendExploratory.Services;
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
        public async Task<(double latitude, double longitude)> Get(string location, CancellationToken canceltoken)
        {
            return await this.geoService.RetrieveLatLong(location, canceltoken);
        }

    }
}
