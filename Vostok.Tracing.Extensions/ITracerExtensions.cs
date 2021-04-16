using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions
{
    [PublicAPI]
    public static class ITracerExtensions
    {
        /// <summary>
        /// <para>Begins construction of a new span which will belong a to new trace and returns a builder responsible for it (see <see cref="ISpanBuilder"/> for details).</para>
        /// <para>Disposing this builder will return back previous trace context.</para>
        /// </summary>
        [NotNull]
        public static ISpanBuilder BeginNewTrace([NotNull] this ITracer tracer) =>
            new NewTraceContextUsing(tracer, null, null);
        
        [NotNull]
        public static ISpanBuilder BeginNewTrace([NotNull] this ITracer tracer, [NotNull] ISpanBuilder currentSpan) =>
            new NewTraceContextUsing(tracer, currentSpan, null);
        
        [NotNull]
        public static ISpanBuilder BeginNewTrace([NotNull] this ITracer tracer, Guid traceId) =>
            new NewTraceContextUsing(tracer, null, traceId);
        
        private class NewTraceContextUsing : ISpanBuilder
        {
            private readonly ITracer tracer;
            private readonly ISpanBuilder builder;
            private readonly TraceContext oldContext;

            public NewTraceContextUsing([NotNull] ITracer tracer, [CanBeNull] ISpanBuilder currentSpan, [CanBeNull] Guid? traceId)
            {
                this.tracer = tracer;

                oldContext = tracer.CurrentContext;

                tracer.CurrentContext = traceId.HasValue ? new TraceContext(traceId.Value, Guid.Empty) : null;

                builder = tracer.BeginSpan();

                if (oldContext != null)
                {
                    builder.SetAnnotation("ParentTraceId", oldContext.TraceId);
                    builder.SetAnnotation("ParentTraceSpanId", oldContext.SpanId);
                }

                currentSpan?.SetAnnotation("ChildTraceId", builder.CurrentSpan.TraceId);
            }

            public void Dispose()
            {
                builder.Dispose();

                tracer.CurrentContext = oldContext;
            }

            public ISpan CurrentSpan => builder.CurrentSpan;

            public void SetAnnotation(string key, object value, bool allowOverwrite = true) =>
                builder.SetAnnotation(key, value, allowOverwrite);

            public void SetBeginTimestamp(DateTimeOffset timestamp) =>
                builder.SetBeginTimestamp(timestamp);

            public void SetEndTimestamp(DateTimeOffset? timestamp) =>
                builder.SetEndTimestamp(timestamp);
        }
    }
}