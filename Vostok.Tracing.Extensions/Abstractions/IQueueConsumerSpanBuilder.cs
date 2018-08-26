using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IQueueConsumerSpanBuilder : ISpanBuilder
    {
        void SetExecutionResult([NotNull] string executionResult);
    }
}