using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MessageSentModel : PageModel
{
    public MessageSentModel() { }

    public ActionResult OnGet()
    {
        return Page();
    }
}