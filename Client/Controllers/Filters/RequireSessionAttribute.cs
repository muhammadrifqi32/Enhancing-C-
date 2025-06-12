using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers.Filters
{
    public class RequireSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var hasAllowAnonymous =
                context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (hasAllowAnonymous)
            {
                base.OnActionExecuting(context);
                return;
            }

            var email = context.HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                context.Result = new RedirectToActionResult("Index", "Accounts", new { sessionExpired = true });
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
