using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class QueueTaskLifecycleEventSpanBuilder : QueueSpanBuilder, IQueueTaskLifecycleEventSpanBuilder
    {
        private readonly string operationName;

        public QueueTaskLifecycleEventSpanBuilder(ISpanBuilder spanBuilder, [NotNull] string operationName)
            : base(spanBuilder)
        {
            this.operationName = operationName ?? throw new ArgumentNullException(nameof(operationName));
        }

        public void SetSourceInfo(Guid traceId, Guid spanId)
        {
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.Source.TraceId, traceId.ToPrettyString());
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.Source.SpanId, spanId.ToPrettyString());
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Operation, operationName);
        }
    }
}