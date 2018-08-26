using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestClusterSpanBuilder : HttpRequestSpanBuilder, IHttpRequestClusterSpanBuilder
    {
        public HttpRequestClusterSpanBuilder(ISpanBuilder spanBuilder, string operationName = null)
            : base(spanBuilder, operationName)
        {
        }

        public void SetClusterDetails(string strategy)
        {
            if (strategy == null)
                throw new ArgumentNullException(nameof(strategy));
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Cluster.Strategy, strategy);
        }

        public void SetClusterStatus(string status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Cluster.Status, status);
        }
    }
}