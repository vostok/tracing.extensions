using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IQueueTaskLifecycleEventSpanBuilder : ISpanBuilder
    {
        void SetSourceInfo(Guid traceId, Guid spanId);
    }
}