public record GetApiKeyResponse
{
    public required string ApiKey { get; set; }
    public required string UserId { get; set; }
    public bool IsActive { get; set; }
}