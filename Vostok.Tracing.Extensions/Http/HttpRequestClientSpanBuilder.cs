using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    internal class HttpRequestClientSpanBuilder : HttpRequestSpanBuilder, IHttpRequestClientSpanBuilder
    {
        public HttpRequestClientSpanBuilder(ISpanBuilder builder, string operationName)
            : base(builder, WellKnownSpanKinds.HttpRequest.Client, operationName)
        {
        }

        public void SetTargetDetails(string targetService, string targetEnvironment)
        {
            if (targetService != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Request.TargetService, targetService);
            }

            if (targetEnvironment != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Request.TargetEnvironment, targetEnvironment);
            }
        }
    }
}