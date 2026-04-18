using System;
using System.Collections.Generic;

namespace EnergyUtilityApp;

public partial class OptionValue
{
    public int Id { get; set; }

    public int ParameterId { get; set; }

    public int? Option { get; set; }

    public string? Value { get; set; }
}
