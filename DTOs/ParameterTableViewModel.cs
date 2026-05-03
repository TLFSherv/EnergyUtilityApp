public record ParameterTableViewModel
{
    public string? Name { get; set; }

    public string? Required { get; set; }

    public string? Type { get; set; }

    public string? DefaultValue { get; set; }

    public string? Options { get; set; }

    public string? Text { get; set; }

    public List<OptionValuesViewModel> OptionValues { get; set; }
}

