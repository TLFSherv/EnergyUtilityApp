using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class ResourceModel : PageModel
{
    public ResourceModel()
    { }

    public ActionResult OnGet()
    {
        return Page();
    }
}