using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Nic.Galaxy.Domain.Exception;

namespace Nic.Galaxy.Api.Attributes
{
    /// <summary>
    /// Attribute for custom exception.
    /// </summary>
    /// <seealso cref="System.Web.Http.Filters.ExceptionFilterAttribute" />
    public class CustomExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HandleExceptions(actionExecutedContext);
        }

        /// <summary>
        /// Handles the exceptions described by actionExecutedContext.
        /// </summary>
        /// <param name="actionExecutedContext">Context for the action executed.</param>
        private static void HandleExceptions(HttpActionExecutedContext actionExecutedContext)
        {
            var validException = actionExecutedContext.Exception as ValidException;
            if (validException != null)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, validException.Message);
            }
        }
    }
}
