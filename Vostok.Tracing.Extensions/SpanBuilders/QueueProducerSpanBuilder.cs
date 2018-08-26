using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class QueueProducerSpanBuilder : QueueSpanBuilder, IQueueProducerSpanBuilder
    {
        private readonly string operationName;

        public QueueProducerSpanBuilder(ISpanBuilder spanBuilder, string operationName)
            : base(spanBuilder)
        {
            this.operationName = operationName;
        }

        public void SetQueueInfo(string type, string topic, Guid? taskId = null)
        {
            SetQueue(type, topic, taskId);

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Operation, operationName ?? $"({type}) put to [{topic}]");
        }

        public void SetActionResult(string actionResult, Guid taskTraceId, Guid? taskId = null)
        {
            if (actionResult == null)
                throw new ArgumentNullException(nameof(actionResult));

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.ActionResult, actionResult);
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.TaskTraceId, taskTraceId.ToPrettyString());

            if (taskId.HasValue)
                SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.TaskId, taskId.ToPrettyString());
        }
    }
}