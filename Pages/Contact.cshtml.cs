using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using EnergyUtilityApp;
public class ContactModel : PageModel
{
    [BindProperty]
    public SendMessageRequest? Input { get; set; }
    private readonly AppDbService _dbService;

    public ContactModel(AppDbService dbService)
    {
        _dbService = dbService;
    }

    public ActionResult OnGet()
    {
        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        // If validation fails, we MUST return Page() to show the errors
        if (!ModelState.IsValid) return Page();

        try
        {
            await _dbService.SaveUserMessage(Input);
            return RedirectToPage("./MessageSent");
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Could not send user message");
            return Page();
        }
    }
}