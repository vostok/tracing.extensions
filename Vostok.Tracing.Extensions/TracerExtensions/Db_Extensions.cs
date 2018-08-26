using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;
using Vostok.Tracing.Extensions.SpanBuilders;

namespace Vostok.Tracing.Extensions.TracerExtensions
{
    [PublicAPI]
    public static class DbExtensions
    {
        [Pure]
        public static IDatabaseSpanBuilder BeginDatabaseSpan(this ITracer tracer, string operationName = null)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Database);
            return new DatabaseSpanBuilder(spanBuilder, operationName);
        }
    }
}