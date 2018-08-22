using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestServerSpanBuilder : HttpRequestSpanBuilder, IHttpRequestServerSpanBuilder
    {
        private readonly ISpanBuilder spanBuilder;

        public HttpRequestServerSpanBuilder(ISpanBuilder spanBuilder)
            : base(spanBuilder)
        {
            this.spanBuilder = spanBuilder;
        }

        public void SetClientDetails(string name, string address)
        {
            spanBuilder.SetAnnotation(AnnotationNames.Http.Client.Name, name);
        }
    }
}