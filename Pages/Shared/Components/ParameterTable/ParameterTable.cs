using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class ParameterTable : ViewComponent
{
    public required List<ParameterTableViewModel> ParameterData { get; set; }
    private readonly AppDbService _dbService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<ParameterTable> _logger;
    public ParameterTable(AppDbService dbService,
    IMemoryCache memoryCache,
    ILogger<ParameterTable> logger)
    {
        _dbService = dbService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        try
        {
            if (!_memoryCache.TryGetValue(CacheKeys.ParameterTable, out List<ParameterTableViewModel>? ParameterData))
            {
                ParameterData = await _dbService.GetParameterTableData();

                if (ParameterData == null)
                {
                    return View("Error");
                }

                _logger.LogDebug("Storing parameter data in cache");

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set(CacheKeys.ParameterTable, ParameterData, cacheEntryOptions);

                _logger.LogInformation("Stored parameter data successfully in cache");
            }
            return View(ParameterData);

        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error storing parameter table data in cache");
            return View(ParameterData);
        }
    }
}