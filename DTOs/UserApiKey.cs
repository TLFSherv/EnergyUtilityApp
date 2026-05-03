using System.ComponentModel.DataAnnotations;
public record UserApiKey
{
    [Display(Name = "API Key")]
    [MaxLength(100)]
    public string? ApiKey { get; set; }
    public string? UserId { get; set; }
    public bool IsActive { get; set; }
}