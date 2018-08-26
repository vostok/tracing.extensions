using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IQueueProducerSpanBuilder : ISpanBuilder
    {
        void SetQueueInfo([NotNull] string type, [NotNull] string topic, [CanBeNull] Guid? taskId = null);

        void SetActionResult([NotNull] string actionResult, Guid taskTraceId, [CanBeNull] Guid? taskId = null);
    }
}