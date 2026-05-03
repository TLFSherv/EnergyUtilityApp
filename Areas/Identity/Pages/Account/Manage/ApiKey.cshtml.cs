using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EnergyUtilityApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class ApiKeyModel : PageModel
    {

        public UserApiKey? Input { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbService _dbService;
        private readonly IMemoryCache _memoryCache;

        public ApiKeyModel(
            AppDbService dbService,
            UserManager<ApplicationUser> userManager,
            IMemoryCache memoryCache)
        {
            _dbService = dbService;
            _userManager = userManager;
            _memoryCache = memoryCache;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            string? userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // get api key from cache
            if (!_memoryCache.TryGetValue($"user:{userId}:apikey", out UserApiKey? userApiKey))
            {
                // read api key from database
                GetApiKeyResponse? apiKeyResponse = await _dbService.GetUserApiKey(userId);
                // if null user hasn't generated a key yet
                if (apiKeyResponse == null) return Page();

                userApiKey = new UserApiKey
                {
                    ApiKey = apiKeyResponse.ApiKey,
                    UserId = apiKeyResponse.UserId,
                    IsActive = apiKeyResponse.IsActive
                };

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(20));
                // store api key in cache
                _memoryCache.Set($"user:{userId}:apikey", userApiKey, cacheEntryOptions);
            }

            Input = userApiKey;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // only allow to create new key if key is invalid
            if (Input != null && Input.IsActive) return Page();

            string? userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            // store new api key in database
            var apiKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            await _dbService.SaveUserApiKey(new CreateApiKeyRequest
            {
                ApiKey = apiKey,
                UserId = userId,
                IsActive = true
            });

            // store new api key in cache
            UserApiKey userApiKey = new UserApiKey
            {
                ApiKey = apiKey,
                UserId = userId,
                IsActive = true
            };
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(20));
            _memoryCache.Set($"user:{userId}:apikey", userApiKey, cacheEntryOptions);
            Input = userApiKey;

            return Page();
        }

    }
}