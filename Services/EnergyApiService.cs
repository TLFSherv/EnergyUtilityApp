using System.Net.Http.Json;
using System.Reflection;
public class EnergyApiService
{
    private HttpClient _httpClient;
    public EnergyApiService()
    {
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
            var response = await _httpClient.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ApiResponse>();
                return data?.Output[0];
            }
            return null;
        }
        catch (Exception ex)
        {
            // log error
            throw;
        }
    }
}