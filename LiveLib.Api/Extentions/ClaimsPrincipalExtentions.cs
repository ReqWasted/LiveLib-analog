using System.Security.Claims;

namespace LiveLib.Api.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
        public static Guid Id(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
