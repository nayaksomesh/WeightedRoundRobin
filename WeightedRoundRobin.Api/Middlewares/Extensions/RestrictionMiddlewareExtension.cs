using Microsoft.AspNetCore.Builder;

namespace WeightedRoundRobin.Api.Middlewares.Extensions
{
    public static class RestrictionMiddlewareExtension
    {
        public static IApplicationBuilder UseCountryRestriction(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CountryRestrictionMiddleware>();
        }
    }
}