using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class QueueConsumerSpanBuilder : QueueSpanBuilder, IQueueConsumerSpanBuilder
    {
        public QueueConsumerSpanBuilder(ISpanBuilder spanBuilder)
            : base(spanBuilder)
        {
        }

        public void SetExecutionResult(string executionResult)
        {
            if (executionResult == null)
                throw new ArgumentNullException(nameof(executionResult));

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Queue.ExecutionResult, executionResult);
        }
    }
}