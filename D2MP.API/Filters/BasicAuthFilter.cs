using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IConfigurationProvider = D2MP.Infrastructure.Interfaces.IConfigurationProvider;

namespace D2MP.API.Filters
{
    public class BasicAuthFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationToken);
            var configProvider = context.HttpContext.RequestServices.GetService<IConfigurationProvider>();
            var acquiredSecret = authorizationToken.ToString();
            var expectedSecret = configProvider.GetBasicAuthSecret();

            if (string.IsNullOrEmpty(acquiredSecret) || acquiredSecret != expectedSecret)
            {
                context.Result = new ObjectResult(context.ModelState)
                {
                    Value = null,
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
            else
            {
                await base.OnActionExecutionAsync(context, next);
            }
        }
    }
}
