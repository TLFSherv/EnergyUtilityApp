using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Caching.Memory;

namespace EnergyUtilityApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    [FromBody]
    public ParameterOptions APIParameters { get; set; } = new();
    public required List<ParameterTableDisplay> ParameterTableData { get; set; }
    public required string EnergyUtilityApiUrl = "http://localhost:5252/api/energy-utility?Postcode=AB10AG";
    private readonly EnergyApiService _apiService;
    private readonly AppDbService _dbService;
    private readonly IMemoryCache _memoryCache;
    public IndexModel(AppDbService dbService, EnergyApiService apiService, IMemoryCache memoryCache)
    {
        _dbService = dbService;
        _apiService = apiService;
        _memoryCache = memoryCache;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!_memoryCache.TryGetValue(CacheKeys.ParameterTable, out List<ParameterTableDisplay>? cachedValue))
            {
                cachedValue = await _dbService.GetParameterTableData();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set(CacheKeys.ParameterTable, cachedValue, cacheEntryOptions);
            }
            ParameterTableData = cachedValue;
            return Page();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _apiService.GetPostcodeEnergyData(EnergyUtilityApiUrl, APIParameters);
            if (result != null)
            {
                return new JsonResult(result);
            }

            return Page();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // log error
            return Page();
        }
    }
}
