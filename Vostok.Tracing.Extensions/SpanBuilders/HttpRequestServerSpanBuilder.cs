using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestServerSpanBuilder : HttpRequestSpanBuilder, IHttpRequestServerSpanBuilder
    {
        public HttpRequestServerSpanBuilder(ISpanBuilder spanBuilder, string operationName)
            : base(spanBuilder, operationName)
        {
        }

        public void SetClientDetails(string name, string address)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Client.Name, name);
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Client.Address, address);
        }
    }
}