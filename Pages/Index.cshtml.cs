using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace EnergyUtilityApp.Pages;

public class IndexModel : PageModel
{
    public required ApiParameterTable ApiParameters { get; set; }
    public void OnGet()
    {
        ApiParameters = new ApiParameterTable();
    }
}
