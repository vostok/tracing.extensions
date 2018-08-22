using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestClusterSpanBuilder : InternalSpanBuilder, IHttpRequestClusterSpanBuilder
    {
        protected readonly ISpanBuilder SpanBuilder;

        public HttpRequestClusterSpanBuilder(ISpanBuilder spanBuilder)
            : base(spanBuilder)
        {
            SpanBuilder = spanBuilder;
        }

        public void SetClusterDetails(string requestStrategy, string status)
        {
            SpanBuilder.SetAnnotation(AnnotationNames.Cluster.RequestStrategy, requestStrategy);
            SpanBuilder.SetAnnotation(AnnotationNames.Cluster.Status, status);
        }

        public void SetRequestDetails(Uri uri, string httpMethodName, int contentLength)
        {
            spanBuilder.SetAnnotation(AnnotationNames.Http.Request.Url, uri.ToString());
            spanBuilder.SetAnnotation(AnnotationNames.Http.Request.Method, httpMethodName);
            spanBuilder.SetAnnotation(AnnotationNames.Http.Request.ContentLength, contentLength.ToString());

            spanBuilder.SetAnnotation(AnnotationNames.Operation, $"({httpMethodName.ToUpper()}): {uri}");
        }

        public void SetResponseDetails(int responseCode, int contentLength)
        {
            spanBuilder.SetAnnotation(AnnotationNames.Http.Response.StatusCode, responseCode.ToString());
            spanBuilder.SetAnnotation(AnnotationNames.Http.Response.ContentLength, contentLength.ToString());
        }
    }
}