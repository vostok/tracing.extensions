using System.Net;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    internal class HttpRequestServerSpanBuilder : HttpRequestSpanBuilder, IHttpRequestServerSpanBuilder
    {
        public HttpRequestServerSpanBuilder(ISpanBuilder builder, string operationName)
            : base(builder, WellKnownSpanKinds.HttpRequest.Server, operationName)
        {
        }

        public void SetClientDetails(string name, IPAddress address)
        {
            if (name != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Client.Name, name);
            }

            if (address != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Client.Address, address);
            }
        }
    }
}