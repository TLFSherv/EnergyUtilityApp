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
}