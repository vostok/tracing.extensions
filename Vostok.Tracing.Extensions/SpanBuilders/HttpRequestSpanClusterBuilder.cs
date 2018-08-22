using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestSpanClusterBuilder : HttpRequestSpanBuilder, IHttpRequestClusterSpanBuilder
    {
        protected readonly ISpanBuilder SpanBuilder;

        public HttpRequestSpanClusterBuilder(ISpanBuilder spanBuilder)
            : base(spanBuilder)
        {
            this.SpanBuilder = spanBuilder;
        }

        public void SetClusterDetails(string requestStrategy, string status)
        {
            SpanBuilder.SetAnnotation(AnnotationNames.Cluster.RequestStrategy, requestStrategy);
            SpanBuilder.SetAnnotation(AnnotationNames.Cluster.Status, status);
        }
    }
}