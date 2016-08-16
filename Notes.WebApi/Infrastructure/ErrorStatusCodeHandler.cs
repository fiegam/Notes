using Nancy.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Notes.Core.Infrastructure;

namespace Notes.WebApi.Infrastructure
{
    public class ErrorStatusCodeHandler : IStatusCodeHandler
    {
        private static ILogger _log = LogProvider.GetLogger();

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var exception = context.Items[NancyEngine.ERROR_EXCEPTION] as Exception;
            if (exception != null)
            {
                _log.Fatal("Unhandled Exception", exception);
            }
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound
            || statusCode == HttpStatusCode.InternalServerError
            || statusCode == HttpStatusCode.Forbidden
            || statusCode == HttpStatusCode.Unauthorized;
        }
    }
}