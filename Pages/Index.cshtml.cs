using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EnergyUtilityApp.Pages;

public class IndexModel : PageModel
{
    public ParameterOptions parameters { get; set; } = new();
    public string Title { get; set; }
    public required ApiParameterTable ParameterTableData { get; set; }
    public required string EnergyUtilityApiUrl = "http://localhost:5252/api/energy-utility?Postcode=AB10AG";
    private EnergyApiService _apiService;
    public IndexModel(EnergyApiService apiService)
    {
        _apiService = apiService;
        // read this data from database
        // cache results using Redis
        ParameterTableData = new ApiParameterTable();
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {

        try
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _apiService.GetPostcodeEnergyData(EnergyUtilityApiUrl, parameters);
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
