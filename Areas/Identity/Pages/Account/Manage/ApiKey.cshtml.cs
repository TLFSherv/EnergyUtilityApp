using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EnergyUtilityApp.Areas.Identity.Pages.Account.Manage
{
    public class ApiKeyModel : PageModel
    {
        [Display(Name = "API Key")]
        [MaxLength(100)]
        public string? ApiKey { get; set; }
        private string? UserId { get; set; }
        public bool IsKeyValid { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbService _dbService;

        public ApiKeyModel(
            AppDbService dbService,
            UserManager<ApplicationUser> userManager)
        {
            _dbService = dbService;
            _userManager = userManager;
            UserId = _userManager.GetUserId(User);
        }
        public async Task OnGetAsync()
        {
            // read api key from database
            var apiKeyResponse = await _dbService.GetUserApiKey(UserId);
            ApiKey = apiKeyResponse.ApiKey;
            IsKeyValid = apiKeyResponse.IsActive;

            // store api key in cache

            // get api key from cache
        }
        public async Task OnPostAsync()
        {
            // only allow to create new key if key is invalid
            if (IsKeyValid) return;

            string key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            // store new api key in database
            await _dbService.SetUserApiKey(new CreateApiKeyRequest
            {
                ApiKey = key,
                UserId = UserId,
                IsActive = true
            });

            // store new api key in cache
        }

    }
}