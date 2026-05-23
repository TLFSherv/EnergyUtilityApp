using System.Net.Http.Headers;
using System.Net.Http.Json;

public class EnergyApiService
{
    private readonly ILogger<EnergyApiService> _logger;
    private readonly PostcodeEnergyClient _energyClient;
    public EnergyApiService(ILogger<EnergyApiService> logger, PostcodeEnergyClient energyClient)
    {
        _logger = logger;
        _energyClient = energyClient;
    }
    public async Task<PostcodeEnergyDataResponse?> GetPostcodeEnergy(ParameterOptionsViewModel parameters)
    {
        try
        {
            _logger.LogDebug("Requesting data from EnergyUtilityApi");
            var data = await _energyClient.GetPostcodeEnergy(parameters);
            _logger.LogInformation("Requested data from EnergyUtilityApi successfully");

            return data;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error requesting data from EnergyUtilityApi");
            return null;
        }
    }


}