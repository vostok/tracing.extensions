using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class QueueTaskLifecycleSpanBuilder : QueueSpanBuilder, IQueueTaskLifecycleSpanBuilder
    {
        public QueueTaskLifecycleSpanBuilder(ISpanBuilder spanBuilder)
            : base(spanBuilder)
        {
        }

        public void SetQueueInfo(string type, string topic, Guid taskId)
        {
            SetQueue(type, topic, taskId);
            SpanBuilder.MakeEndless();
        }
    }
}