using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
public class ContactModel : PageModel
{
    [BindProperty]
    public SendMessage? Input { get; set; }
    public ContactModel()
    {

    }

    public ActionResult OnGet()
    {
        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // If validation fails, we MUST return Page() to show the errors
            return Page();
        }
        return Page();
    }
}