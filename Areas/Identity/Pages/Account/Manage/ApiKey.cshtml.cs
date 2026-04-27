using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using EnergyUtilityApp.HelperClass;

namespace EnergyUtilityApp.Areas.Identity.Pages.Account.Manage
{
    public class ApiKeyModel : PageModel
    {
        [Display(Name = "API Key")]
        [MaxLength(100)]
        public string? ApiKey { get; set; }

        public bool IsKeyValid { get; set; }

        private readonly AppDbService _dbService;

        public ApiKeyModel(AppDbService dbService)
        {
            _dbService = dbService;
        }
        public async Task OnGetAsync()
        {
            // read api key from database
            if (ApiKey == null)
            {

            }
        }
        public async Task OnPostAsync()
        {
            if (IsKeyValid) return;

            var key = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            using (SHA256 mySHA256 = SHA256.Create())
            {
                string hash = Hashing.GetHash(mySHA256, key);
                // store new api key hash in database

                // store new api key in cache
            }
            ApiKey = key;
            IsKeyValid = true;
        }


    }
}