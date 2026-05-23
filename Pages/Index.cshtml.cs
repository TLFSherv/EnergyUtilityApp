using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace EnergyUtilityApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    [FromBody]
    public ParameterOptionsViewModel APIParameters { get; set; } = new();
    public required List<ParameterTableViewModel> ParameterData { get; set; }
    public required string EnergyUtilityApiUrl { get; set; }
    private readonly EnergyApiService _apiService;
    private readonly AppDbService _dbService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<IndexModel> _logger;
    private readonly IOptions<AppServiceSettings> _options;
    public IndexModel(AppDbService dbService,
    EnergyApiService apiService,
    IMemoryCache memoryCache,
    ILogger<IndexModel> logger,
    IOptions<AppServiceSettings> options)
    {
        _dbService = dbService;
        _apiService = apiService;
        _memoryCache = memoryCache;
        _logger = logger;
        _options = options;
        EnergyUtilityApiUrl = _options.Value.EnergyUtilityApiUrl;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!_memoryCache.TryGetValue(CacheKeys.ParameterTable, out List<ParameterTableViewModel>? parameterData))
            {
                parameterData = await _dbService.GetParameterTableData();

                _logger.LogDebug("Storing parameter data in cache");

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set(CacheKeys.ParameterTable, parameterData, cacheEntryOptions);

                _logger.LogInformation("Stored parameter data successfully in cache");
            }
            ParameterData = parameterData;
            return Page();
        }
        catch (Exception)
        {
            _logger.LogWarning("Problem storing parameter data in cache");
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid parameter options");
            return Page();
        }

        var result = await _apiService.GetPostcodeEnergy(APIParameters);
        if (result != null)
        {
            return new JsonResult(result);
        }

        return Page();
    }
}
