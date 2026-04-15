using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace EnergyUtilityApp.Pages;

public class IndexModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }
    public required ApiParameterTable ParameterTableData { get; set; }
    public void OnGet([FromQuery] string? search)
    {
        ParameterTableData = new ApiParameterTable();
        if (string.IsNullOrWhiteSpace(search)) return;

        SearchTerm = search;
        var searchTerm = search.Trim().ToLowerInvariant();

        ParameterTableData.Rows = ParameterTableData.Rows
            .Where(row =>
                row.Parameter?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                row.Description?.Any(desc =>
                    desc?.Description?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true) == true)
            .ToList();

    }
}
