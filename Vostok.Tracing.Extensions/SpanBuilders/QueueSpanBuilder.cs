using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class QueueSpanBuilder : InternalSpanBuilder
    {
        public QueueSpanBuilder(ISpanBuilder spanBuilder)
            : base(spanBuilder)
        {
        }

        protected void SetQueue([NotNull] string type, [NotNull] string topic, Guid? taskId)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (topic == null)
                throw new ArgumentNullException(nameof(topic));

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.Type, type);
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.Topic, topic);

            if (taskId.HasValue)
                SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.TaskId, taskId.ToPrettyString());
        }
    }
}