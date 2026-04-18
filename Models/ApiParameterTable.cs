public class ApiParameterTable
{
    public List<ApiParameterTableRow> Rows { get; set; }

    public ApiParameterTable()
    {
        Rows = new List<ApiParameterTableRow>
        {
            new ApiParameterTableRow
            (
                "Postcode",
                "Yes",
                new Dictionary<string, string>()
                {
                    {"Type:", "string"},
                    {"Example:", "AB10AG"}
                },
                new List<DescriptionColumn>{
                    new (null,"The postcode you want the data for. Do not include spaces.")
                }
            ),
            new ApiParameterTableRow
            (
                "PropertyType",
                "No",
                new Dictionary<string, string>()
                {
                    {"Type:", "Integer"},
                    {"Default:", "None"},
                    {"Options:", "1,2,3,4,5,6"}
                },
                new List<DescriptionColumn>{
                    new (1, "Detached"),
                    new (2, "Semi detached"),
                    new (3, "Mid terrace"),
                    new (4, "End terrace"),
                    new (5, "Bungalow"),
                    new (6, "Flat"),
                }
            ),
            new ApiParameterTableRow(
                "PropertyAge",
                "No",
                new Dictionary<string, string>()
                {
                    {"Type:", "Integer"},
                    {"Default:", "None"},
                    {"Options:", "1,2,3,4"}
                },
                new List<DescriptionColumn>{
                    new (1, "before 1930"),
                    new (2, "1930-1972"),
                    new (3, "1973-1999"),
                    new (4, "2000 or later")
                }
            ),
            new ApiParameterTableRow(
                "FloorArea",
                "No",
                new Dictionary<string, string>()
                {
                    {"Type:", "Integer"},
                    {"Default:", "None"},
                    {"Options:", "1,2,3,4,5"}
                },
                new List<DescriptionColumn>{
                    new (1, "50 or less"),
                    new (2, "51 - 100"),
                    new (3, "101 - 150"),
                    new (4, "151 - 200"),
                    new (5, "over 200")
                }
            ),
            new ApiParameterTableRow(
                "HouseholdSize",
                "No",
                new Dictionary<string, string>()
                {
                    {"Type:", "Integer"},
                    {"Default:", "None"},
                    {"Options:", "1,2,3"}
                },
                new List<DescriptionColumn>{
                    new (1, "1-2 People"),
                    new (2, "2-3 People"),
                    new (3, "4+ People")
                }
            )
        };
    }
}

public record ApiParameterTableRow(string Parameter, string Required, Dictionary<string, string> Value, List<DescriptionColumn> Description);
public record DescriptionColumn(int? Option, string Description);