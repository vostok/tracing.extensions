using System;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    public interface IHttpRequestClusterSpanBuilder : IHttpRequestSpanBuilder
    {
        void SetClusterDetails(string requestStrategy, string status);
    }
}