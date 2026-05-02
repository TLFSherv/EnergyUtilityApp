using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
public class EnergyApiService
{
    private readonly HttpClient _httpClient;

    private readonly IConfiguration _config;
    public EnergyApiService(IConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient();
    }
    public async Task<PostcodeEnergyDataResponse?> GetPostcodeEnergyData(string apiUrl, ParameterOptions parameters)
    {
        try
        {
            string requestUrl = apiUrl;
            Type type = parameters.GetType();
            // Get all public properties of the object's type
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                // get the name and value of each property
                string name = property.Name;
                object value = property.GetValue(parameters);

                if (value is int val && val != 0)
                {
                    requestUrl += $"&{name}={value}";
                }
            }
            string apiKey = _config["ApiKeys:EnergyUtilityApi"];
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return data?.Output[0];

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}