using System;
using System.Collections.Generic;

namespace EnergyUtilityApp;

public partial class ParameterTable
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Required { get; set; }

    public string? Type { get; set; }

    public string? DefaultValue { get; set; }

    public string? Options { get; set; }
}
