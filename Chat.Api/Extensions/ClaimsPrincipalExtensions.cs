using System.Security.Claims;
using Chat.Infrastructure.Constants;

namespace Chat.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetId(this ClaimsPrincipal userClaimsPrincipal)
    {
        var idString = userClaimsPrincipal.FindFirst(ClaimTypes.PrimarySid)?.Value;
                       //?? userClaimsPrincipal.FindFirst(CustomClaimTypes.Index)?.Value;

        return Guid.TryParse(idString, out var id) ? id : null;
    }
}