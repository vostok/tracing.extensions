﻿using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions
{
    [PublicAPI]
    public static class ITracerExtensions
    {
        [NotNull]
        public static ISpanBuilder BeginNewTrace([NotNull] this ITracer tracer) =>
            new NewTraceContextUsing(tracer);

        private class NewTraceContextUsing : ISpanBuilder
        {
            private readonly ITracer tracer;
            private readonly ISpanBuilder builder;
            private readonly TraceContext oldContext;

            public NewTraceContextUsing(ITracer tracer)
            {
                this.tracer = tracer;

                oldContext = tracer.CurrentContext;

                tracer.CurrentContext = null;

                builder = tracer.BeginSpan();

                if (oldContext != null)
                {
                    builder.SetAnnotation("ParentTraceId", oldContext.TraceId);
                    builder.SetAnnotation("ParentTraceSpanId", oldContext.SpanId);
                }
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