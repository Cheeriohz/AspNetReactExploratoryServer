﻿using AspnetCoreBackendExploratory.Services.Contracts;
using AspnetCoreBackendExploratory.Services.Contracts.PositionAPIForwardRecords;
using System.Collections.Concurrent;
using System.IO.Compression;
using System.Text.Json;

namespace AspnetCoreBackendExploratory.Services
{
    public class GeoCodeForwardingService
    {
        public GeoCodeForwardingService(string apiKey) => this.apiKey = apiKey;


        private readonly ConcurrentDictionary<string, (double latitude, double longiture)> cachedLocations = new (StringComparer.OrdinalIgnoreCase);

        private readonly string apiKey;
        public async Task<(double latitude, double longitude)> RetrieveLatLong(string location, CancellationToken cancellationToken)
        {
            if(this.cachedLocations.TryGetValue(location, out (double latitude, double longitude) latlong))
                return latlong;

            using HttpClient client = new()
            {
                BaseAddress = new Uri($"http://api.positionstack.com/v1/forward")
            };

            HttpResponseMessage response = await client.GetAsync($"?access_key={this.apiKey}&query={location}", cancellationToken);

            AggregateData aggregateData = (response.Content.Headers.TryGetValues("Content-Encoding", out IEnumerable<string>? values) 
                    ? values.FirstOrDefault()
                    : string.Empty) switch
            {
                "gzip" => await GZipDecompressResponse(response, cancellationToken),
                _ => JsonSerializer.Deserialize<AggregateData>(await response.Content.ReadAsStreamAsync(cancellationToken)) 
            } ?? new AggregateData(Array.Empty<ForwardData>());
 
            if(aggregateData.data.Length > 0)
            {
                (double longitude, double latitude) value = (aggregateData.data[0].longitude, aggregateData.data[0].latitude);
                this.cachedLocations[location] = value;

                return value;
            }

            return (0, 0);

        }


        private async static Task<AggregateData?> GZipDecompressResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            using Stream contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            {
                using GZipStream decompressionStream = new (contentStream, CompressionMode.Decompress);
                {
                    AggregateData? aggregateData = JsonSerializer.Deserialize<AggregateData>(decompressionStream);
                    return aggregateData;
                }
            }
        }
    }
}
