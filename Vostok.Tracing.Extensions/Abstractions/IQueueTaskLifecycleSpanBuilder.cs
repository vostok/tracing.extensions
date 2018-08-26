using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IQueueTaskLifecycleSpanBuilder : ISpanBuilder
    {
        void SetQueueInfo([NotNull] string type, [NotNull] string topic, Guid taskId);
    }
}