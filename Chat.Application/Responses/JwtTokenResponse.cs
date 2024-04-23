namespace Chat.Application.Responses;

public record JwtTokenResponse(string? Token, DateTime? ValidTo, bool Success);