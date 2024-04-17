namespace Chat.Application.Responses;

public class JwtTokenResponse
{
    public string? Token { get; set; }
    public DateTime? ValidTo { get; set; }
    public bool Success { get; init; }
}