using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
public class EnergyApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly ILogger<EnergyApiService> _logger;
    public EnergyApiService(IConfiguration config, ILogger<EnergyApiService> logger)
    {
        _config = config;
        _httpClient = new HttpClient();
        _logger = logger;
    }
    public async Task<PostcodeEnergyDataResponse?> GetPostcodeEnergyData(string apiUrl, ParameterOptionsViewModel parameters)
    {
        string requestUrl = GenerateRequestUrl(apiUrl, parameters);
        string? apiKey = _config["ApiKeys:EnergyUtilityApi"];
        try
        {
            _httpClient.DefaultRequestHeaders.Remove("X-API-KEY");
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            _logger.LogDebug("Requesting data from EnergyUtilityApi with URL: {urlRequst}", requestUrl);

            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<EnergyUtilityApiResponse>();
            _logger.LogInformation("Requested data from EnergyUtilityApi successfully for URL: {urlRequest}", requestUrl);
            return data?.Output[0];
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error requesting data from EnergyUtilityApi for URL: {urlRequest}", requestUrl);
            return null;
        }
    }

    private string GenerateRequestUrl(string url, ParameterOptionsViewModel parameters)
    {
        string requestUrl = url;
        Type type = parameters.GetType();
        // Get all public properties of the object's type
        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            // get the name and value of each property
            string name = property.Name;
            object? value = property.GetValue(parameters);

            if (value is int val && val != 0)
            {
                requestUrl += $"&{name}={value}";
            }
        }
        return requestUrl;
    }
}