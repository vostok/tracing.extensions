using System;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    internal class HttpRequestClusterSpanBuilder : HttpRequestSpanBuilder, IHttpRequestClusterSpanBuilder
    {
        public HttpRequestClusterSpanBuilder(ISpanBuilder builder, string operationName)
            : base(builder, WellKnownSpanKinds.HttpRequest.Cluster, operationName)
        {
        }

        public void SetTargetDetails(string targetService, string targetEnvironment)
        {
            if (targetService != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Request.TargetService, targetService);
            }

            if (targetEnvironment != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Request.TargetEnvironment, targetEnvironment);
            }
        }

        public void SetClusterStrategy(string strategyName)
        {
            if (strategyName == null)
                throw new ArgumentNullException(nameof(strategyName));

            SetAnnotation(WellKnownAnnotations.Http.Cluster.Strategy, strategyName);
        }

        public void SetClusterStatus(string status)
        {
            if (status == null)
                throw new ArgumentNullException(nameof(status));

            SetAnnotation(WellKnownAnnotations.Http.Cluster.Status, status);
        }
    }
}