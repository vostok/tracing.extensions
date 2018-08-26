using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;
using Vostok.Tracing.Extensions.SpanBuilders;

namespace Vostok.Tracing.Extensions.TracerExtensions
{
    [PublicAPI]
    public static class QueueExtensions
    {
        [Pure]
        public static IQueueProducerSpanBuilder BeginQueueProducerSpan(this ITracer tracer, string operationName = null)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.Producer);
            return new QueueProducerSpanBuilder(spanBuilder, operationName);
        }

        [Pure]
        public static IQueueTaskLifecycleSpanBuilder BeginQueueTaskLifecycleSpan(this ITracer tracer)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.TaskLifecycle);
            return new QueueTaskLifecycleSpanBuilder(spanBuilder);
        }

        [Pure]
        public static IQueueTaskLifecycleEventSpanBuilder BeginQueueTaskLifecycleEventSpan(this ITracer tracer, [NotNull] string operationName)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.TaskLifecycleEvent);
            return new QueueTaskLifecycleEventSpanBuilder(spanBuilder, operationName);
        }

        [Pure]
        public static IQueueManagerSpanBuilder BeginQueueManagerEventSpan(this ITracer tracer, [NotNull] string operationName)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.Manager);
            return new QueueManagerSpanBuilder(spanBuilder, operationName);
        }

        [Pure]
        public static IQueueConsumerSpanBuilder BeginQueueConsumerSpan(this ITracer tracer)
        {
            var spanBuilder = tracer.BeginSpan();
            spanBuilder.SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.Consumer);
            return new QueueConsumerSpanBuilder(spanBuilder);
        }
    }
}