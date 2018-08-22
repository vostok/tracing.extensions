using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestServerSpanBuilder : InternalSpanBuilder, IHttpRequestServerSpanBuilder
    {
        private readonly ISpanBuilder spanBuilder;

        public HttpRequestServerSpanBuilder(ISpanBuilder spanBuilder)
            : base(spanBuilder)
        {
            this.spanBuilder = spanBuilder;
        }

        public void SetClientDetails(string name, string address)
        {
            spanBuilder.SetAnnotation(AnnotationNames.Http.Client.Name, name);
            spanBuilder.SetAnnotation(AnnotationNames.Http.Client.Address, address);
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