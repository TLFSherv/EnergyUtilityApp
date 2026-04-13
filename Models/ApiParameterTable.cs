public class ApiParameterTable
{
    public List<OptionDesc> PropteryType { get; set; }
    public List<OptionDesc> PropteryAge { get; set; }
    public List<OptionDesc> FloorArea { get; set; }
    public List<OptionDesc> HouseholdSize { get; set; }

    public ApiParameterTable()
    {
        PropteryType = new List<OptionDesc>
        {
            new (1, "Detached"),
            new (2, "Semi detached"),
            new (3, "Mid terrace"),
            new (4, "End terrace"),
            new (5, "Bungalow"),
            new (6, "Flat"),
        };

        PropteryAge = new List<OptionDesc>
        {
            new (1, "before 1930"),
            new (2, "1930-1972"),
            new (3, "1973-1999"),
            new (4, "2000 or later")
        };

        FloorArea = new List<OptionDesc>
        {
            new (1, "50 or less"),
            new (2, "51 - 100"),
            new (3, "101 - 150"),
            new (4, "151 - 200"),
            new (5, "over 200")
        };

        HouseholdSize = new List<OptionDesc>
        {
            new (1, "1-2 People"),
            new (2, "2-3 People"),
            new (3, "4+ People")
        };
    }
}
public record OptionDesc(int Option, string Description);