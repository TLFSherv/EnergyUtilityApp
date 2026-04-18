using System.ComponentModel.DataAnnotations;
public class ParameterOptions
{
    [Range(1, 6)]
    public int? PropertyType { get; set; }
    [Range(1, 4)]
    public int? PropertyAge { get; set; }
    [Range(1, 5)]
    public int? FloorArea { get; set; }
    [Range(1, 3)]
    public int? HouseholdSize { get; set; }
}