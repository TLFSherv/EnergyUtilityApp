public record PostcodeEnergyDataResponse
{
    public required string Postcode { get; set; }
    public decimal EnergyConsumption { get; set; }
    public decimal EnergyCost { get; set; }
    public required EnergyMeterData MeterData { get; set; }
    public required EnergyRegionData RegionData { get; set; }
};

public record EnergyMeterData
{
    public int? NumOfMeters { get; set; }
    public decimal? MeanCons { get; set; }
    public decimal? MedianCons { get; set; }
    public decimal? TotalCons { get; set; }
}

public record EnergyRegionData
{
    public char? RegionCode { get; set; }
    public string? Region { get; set; }
    public string? Operator { get; set; }
}