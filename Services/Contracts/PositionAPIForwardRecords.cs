namespace AspnetCoreBackendExploratory.Services.Contracts.PositionAPIForwardRecords
{

    public record AggregateData(ForwardData[] data);

    public record ForwardData(
        
        double latitude,
        double longitude,
        string label,
        string name,
        string type,
        string number,
        string street,
        string postal_code,
        int confidence,
        string region,
        string region_code,
        string administrative_area,
        string neighbourhood,
        string country,
        string country_code,
        string map_url     
        
        );


        
        
}
