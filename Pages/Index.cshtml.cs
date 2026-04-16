using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnergyUtilityApp.Pages;

public class IndexModel : PageModel
{
    public required ApiParameterTable ParameterTableData { get; set; }
    public void OnGet()
    {

        ParameterTableData = new ApiParameterTable();
    }
}
