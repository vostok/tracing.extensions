using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;
using Vostok.Tracing.Extensions.SpanBuilders;

namespace Vostok.Tracing.Extensions
{
    public static class HttpRequestExtensions
    {
        public static IHttpRequestSpanBuilder BeginHttpRequestClientSpan(this ITracer tracer)
        {
            var spanBuilder = tracer.BeginSpan();

            spanBuilder.SetAnnotation(AnnotationNames.SpanKind, "http-request-client");

            return new HttpRequestClientSpanBuilder(spanBuilder);
        }

        public static IHttpRequestServerSpanBuilder BeginHttpRequestServerSpan(this ITracer tracer)
        {
            var spanBuilder = tracer.BeginSpan();

            spanBuilder.SetAnnotation(AnnotationNames.SpanKind, "http-request-server");

            return new HttpRequestServerSpanBuilder(spanBuilder);
        }

        public static IHttpRequestClusterSpanBuilder BeginHttpRequestClusterSpan(this ITracer tracer)
        {
            var spanBuilder = tracer.BeginSpan();

            spanBuilder.SetAnnotation(AnnotationNames.SpanKind, "http-request-cluster");

            return new HttpRequestClusterSpanBuilder(spanBuilder);
        }
    }
}