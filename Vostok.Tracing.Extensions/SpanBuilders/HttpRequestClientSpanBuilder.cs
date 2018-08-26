using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestClientSpanBuilder : HttpRequestSpanBuilder, IHttpRequestClientSpanBuilder
    {
        public HttpRequestClientSpanBuilder(ISpanBuilder spanBuilder, string operationName = null)
            : base(spanBuilder, operationName)
        {
        }
    }
}