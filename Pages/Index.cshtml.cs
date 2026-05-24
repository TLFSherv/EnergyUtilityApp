using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace EnergyUtilityApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    [FromBody]
    public ParameterOptionsViewModel APIParameters { get; set; } = new();
    public required string EnergyUtilityApiUrl { get; set; }
    private readonly EnergyApiService _apiService;
    private readonly ILogger<IndexModel> _logger;
    private readonly IOptions<AppServiceSettings> _options;
    public IndexModel(
    EnergyApiService apiService,
    ILogger<IndexModel> logger,
    IOptions<AppServiceSettings> options)
    {
        _apiService = apiService;
        _logger = logger;
        _options = options;
    }

    public IActionResult OnGet()
    {
        EnergyUtilityApiUrl = _options.Value.EnergyUtilityApiUrl;
        return Page();
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
