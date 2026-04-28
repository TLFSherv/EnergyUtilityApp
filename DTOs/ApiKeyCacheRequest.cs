public record ApiKeyCacheRequest
{
    public required string ApiKey;
    public required string UserId;
    public required bool IsActive;
}