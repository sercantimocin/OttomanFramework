namespace Ottoman.Core.Infrastructure
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class OttomanApiValidationFilter : ActionFilterAttribute
    {
        private readonly HttpStatusCode _statusCodeForValidation;

        public OttomanApiValidationFilter(HttpStatusCode statusCodeForValidation)
        {
            _statusCodeForValidation = statusCodeForValidation;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(_statusCodeForValidation, actionContext.ModelState);
            }
        }
    }
}
