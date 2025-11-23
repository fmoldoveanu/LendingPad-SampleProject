using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Raven.Client;

namespace WebApi.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ContextInitializeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var method = actionExecutedContext.Request.Method;

            // Only commit on write operations
            if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
            {
                var response = actionExecutedContext.Response;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var container = GlobalConfiguration.Configuration.DependencyResolver;
                    var session = container.GetService(typeof(IDocumentSession)) as IDocumentSession;

                    if (session != null)
                    {
                        try
                        {
                            session.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            // Optionally log the exception or wrap it in an HttpResponseException
                            throw new HttpResponseException(
                                actionExecutedContext.Request.CreateErrorResponse(
                                    System.Net.HttpStatusCode.InternalServerError,
                                    $"Error saving changes: {ex.Message}"
                                )
                            );
                        }
                    }
                }
            }
        }
    }
}
