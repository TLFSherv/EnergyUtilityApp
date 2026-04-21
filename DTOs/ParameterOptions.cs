using System.ComponentModel.DataAnnotations;
public class ParameterOptions
{
    [Range(1, 7)]
    public int? PropertyType { get; set; }
    [Range(1, 12)]
    public int? PropertyAge { get; set; }
    [Range(1, 5)]
    public int? FloorArea { get; set; }
    [Range(1, 3)]
    public int? HouseholdSize { get; set; }
    [Range(1, 5)]
    public int? NumberOfAdults { get; set; }
    [Range(1, 4)]
    public int? NumberOfBedrooms { get; set; }
    [Range(1, 3)]
    public int? PaymentMethod { get; set; }
    [Range(1, 2)]
    public int? MeterType { get; set; }
}