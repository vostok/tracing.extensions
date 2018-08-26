using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IHttpRequestClusterSpanBuilder : IHttpRequestSpanBuilder
    {
        void SetClusterDetails([NotNull] string strategy);
        void SetClusterStatus([NotNull] string status);
    }
}