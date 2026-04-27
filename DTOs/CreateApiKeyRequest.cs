using System.ComponentModel.DataAnnotations;
public record CreateApiKeyRequest
{
    [Required]
    [MaxLength(100)]
    public string? ApiKey { get; set; }
    [Required]
    public string? UserId { get; set; }
    public bool IsActive { get; set; } = true;
}