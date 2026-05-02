using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using EnergyUtilityApp;
public class ContactModel : PageModel
{
    [BindProperty]
    public SendMessage? Input { get; set; }
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
        if (!ModelState.IsValid)
        {
            // If validation fails, we MUST return Page() to show the errors
            return Page();
        }

        try
        {
            await _dbService.InsertUserMessage(Input);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return RedirectToPage("./MessageSent");
    }
}