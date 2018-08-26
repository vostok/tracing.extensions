using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class QueueManagerSpanBuilder : QueueSpanBuilder, IQueueManagerSpanBuilder
    {
        private readonly string operationName;

        public QueueManagerSpanBuilder(ISpanBuilder spanBuilder, [NotNull] string operationName)
            : base(spanBuilder)
        {
            this.operationName = operationName ?? throw new ArgumentNullException(nameof(operationName));
        }

        public void SetQueueInfo(string type, string topic, Guid taskId)
        {
            SetQueue(type, topic, taskId);
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Operation, operationName);
        }

        public void SetActionResult(string actionResult)
        {
            if (actionResult == null)
                throw new ArgumentNullException(nameof(actionResult));

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.ActionResult, actionResult);
        }
    }
}