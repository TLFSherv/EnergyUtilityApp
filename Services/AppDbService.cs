using Microsoft.EntityFrameworkCore;
using EnergyUtilityApp;

public class AppDbService
{
    private readonly EnergyUtilityAppDbContext _context;
    private readonly ILogger<AppDbService> _logger;
    public AppDbService(EnergyUtilityAppDbContext context, ILogger<AppDbService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ParameterTableViewModel>?> GetParameterTableData()
    {
        try
        {
            _logger.LogDebug("Fetching parameter table data from database");

            var tableData = await _context.ParameterTables
            .Select(param => new ParameterTableViewModel
            {
                Name = param.Name,
                Required = param.Required,
                Type = param.Type,
                DefaultValue = param.DefaultValue,
                Options = param.Options,
                Text = param.Text,
                OptionValues = _context.OptionValues
                    .Where(opt => param.Id == opt.ParameterId)
                    .Select(opt => new OptionValuesViewModel
                    {
                        ParameterId = opt.ParameterId,
                        Option = opt.Option,
                        Value = opt.Value
                    })
                    .OrderBy(opt => opt.Option)
                    .ToList()
            })
            .ToListAsync();

            _logger.LogInformation("Selected parameter table data from database successfully");

            return tableData;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch parameter table data from database");
            return null;
        }

    }

    public async Task<GetApiKeyResponse?> GetUserApiKey(string userId)
    {
        try
        {
            _logger.LogDebug("Fetching API Key for user with ID: {userId}", userId);

            var apiKey = await _context.UserApiKeys
            .Where(x => x.UserId == userId)
            .Select(x => new GetApiKeyResponse
            {
                ApiKey = x.ApiKey,
                UserId = x.UserId,
                IsActive = x.IsActive
            }).SingleOrDefaultAsync();

            _logger.LogInformation("Fetched API Key successfully for user with ID: {userId}", userId);

            return apiKey;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while fetching API Key for user with ID: {userId}", userId);
            return null;
        }

    }
    public async Task SaveUserApiKey(CreateApiKeyRequest req)
    {
        try
        {
            _logger.LogDebug("Inserting API key for user with ID: {userId}", req.UserId);

            _context.Add(new ApiKeyLookup
            {
                ApiKey = req.ApiKey,
                UserId = req.UserId,
                IsActive = req.IsActive
            });
            await _context.SaveChangesAsync();

            _logger.LogInformation("Inserted successfully API key for user with ID: {userId}", req.UserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting API key for user with ID: {userId}", req.UserId);
        }

    }
    public async Task SaveUserMessage(SendMessageRequest messageRequest)
    {
        try
        {
            _logger.LogDebug("Inserting user message into database for sender: {email}", messageRequest.Email);
            _context.Add(new UserMessage
            {
                Email = messageRequest.Email,
                Message = messageRequest.Message
            });

            _logger.LogDebug("Inserted user message into database successfully for sender: {email}", messageRequest.Email);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting user message into database for sender: {email}", messageRequest.Email);
        }
    }

}