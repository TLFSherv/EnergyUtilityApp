using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
public class NavigationMenu : ViewComponent
{
    private readonly UserManager<ApplicationUser> _userManager;
    public string? UserId;
    public NavigationMenu(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return View("Unauthenticated");
        }

        //UserId = _userManager.GetUserId(HttpContext.User);
        return View();
    }
}