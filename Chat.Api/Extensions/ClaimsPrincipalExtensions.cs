using System.Security.Claims;
using Chat.Infrastructure.Constants;

namespace Chat.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetId(this ClaimsPrincipal userClaimsPrincipal)
    {
        var idString = userClaimsPrincipal.FindFirst(CustomClaimTypes.Id)?.Value;

        return Guid.TryParse(idString, out var id) ? id : null;
    }
}