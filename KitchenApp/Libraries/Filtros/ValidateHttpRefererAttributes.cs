using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KitchenApp.Libraries.Filtros
{
    public class ValidateHttpRefererAttributes : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referer))
            {
                context.Result = new ContentResult() { Content = "Acesso negado." };
            }
            else
            {
                Uri uri = new Uri(referer);

                string hostReferer = uri.Host;
                string hostServer = context.HttpContext.Request.Host.Host;

                if (hostReferer != hostServer)
                {
                    context.Result = new ContentResult() { Content = "Acesso Negado." };
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //TODO
        }
    }
}