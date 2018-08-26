using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;
using Vostok.Tracing.Extensions.SpanBuilders;

namespace Vostok.Tracing.Extensions.TracerExtensions
{
    [PublicAPI]
    public static class HttpRequestExtensions
    {
        [Pure]
        public static IHttpRequestClientSpanBuilder BeginHttpRequestClientSpan(this ITracer tracer, string operationName = null)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.HttpRequest.Client);
            return new HttpRequestClientSpanBuilder(spanBuilder, operationName);
        }

        [Pure]
        public static IHttpRequestServerSpanBuilder BeginHttpRequestServerSpan(this ITracer tracer, string operationName = null)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.HttpRequest.Server);
            return new HttpRequestServerSpanBuilder(spanBuilder, operationName);
        }

        [Pure]
        public static IHttpRequestClusterSpanBuilder BeginHttpRequestClusterSpan(this ITracer tracer, string operationName = null)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.HttpRequest.Cluster);
            return new HttpRequestClusterSpanBuilder(spanBuilder, operationName);
        }
    }
}