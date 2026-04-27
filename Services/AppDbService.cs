using Microsoft.EntityFrameworkCore;
using EnergyUtilityApp;

public class AppDbService
{
    private readonly EnergyUtilityAppDbContext _context;
    public AppDbService(EnergyUtilityAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ParameterTableDisplay>> GetParameterTableData()
    {
        return await _context.ParameterTables
        .Select(param => new ParameterTableDisplay
        {
            Name = param.Name,
            Required = param.Required,
            Type = param.Type,
            DefaultValue = param.DefaultValue,
            Options = param.Options,
            Text = param.Text,
            OptionValues = _context.OptionValues
                .Where(opt => param.Id == opt.ParameterId)
                .Select(opt => new OptionValuesDisplay
                {
                    ParameterId = opt.ParameterId,
                    Option = opt.Option,
                    Value = opt.Value
                })
                .OrderBy(opt => opt.Option)
                .ToList()
        })
        .ToListAsync();
    }

    public async Task<GetApiKeyResponse?> GetUserApiKey(string userId)
    {
        return await _context.UserApiKeys
        .Where(x => x.UserId == userId)
        .Select(x => new GetApiKeyResponse
        {
            ApiKey = x.ApiKey,
            UserId = x.UserId,
            IsActive = x.IsActive
        }).SingleOrDefaultAsync();
    }
    public async Task SetUserApiKey(CreateApiKeyRequest req)
    {
        _context.Add(new ApiKeyLookup
        {
            ApiKey = req.ApiKey,
            UserId = req.UserId,
            IsActive = req.IsActive
        });
        await _context.SaveChangesAsync();
    }

}