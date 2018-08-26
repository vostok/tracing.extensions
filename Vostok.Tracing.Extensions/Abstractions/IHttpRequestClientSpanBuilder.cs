using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IHttpRequestClientSpanBuilder : IHttpRequestSpanBuilder
    {
    }
}