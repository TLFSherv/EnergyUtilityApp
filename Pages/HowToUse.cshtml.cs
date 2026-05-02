using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class HowToUseModel : PageModel
{
    public HowToUseModel()
    {

    }

    public ActionResult OnGet()
    {
        return Page();
    }
}