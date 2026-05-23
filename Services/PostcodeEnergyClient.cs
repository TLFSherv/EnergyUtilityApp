using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Reflection;
public class PostcodeEnergyClient
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly IOptions<AppServiceSettings> _options;
    public PostcodeEnergyClient(IConfiguration config,
    IOptions<AppServiceSettings> options,
    HttpClient client)
    {
        _config = config;
        _options = options;
        _client = client;
        // config typed client
        _client.DefaultRequestHeaders.Add("X-API-KEY", _config["ApiKeys:EnergyUtilityApi"]);
    }
    public async Task<PostcodeEnergyDataResponse?> GetPostcodeEnergy(ParameterOptionsViewModel parameters)
    {
        string? baseUri = _options.Value.EnergyUtilityApiUrl;
        if (baseUri == null) return null;

        Dictionary<string, string?> queryParams = CreateQueryParams(parameters);
        string uriWithQuery = QueryHelpers.AddQueryString(baseUri, queryParams);

        var response = await _client.GetAsync(uriWithQuery);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<EnergyUtilityApiResponse>();
        return data?.Output[0];
    }

    private Dictionary<string, string?> CreateQueryParams(ParameterOptionsViewModel parameters)
    {
        Dictionary<string, string?> queryParams = new Dictionary<string, string?>();
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
                queryParams.Add(name, val.ToString());
            }
        }
        return queryParams;
    }

}