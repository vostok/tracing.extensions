using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class HttpRequestSpanBuilder : InternalSpanBuilder, IHttpRequestSpanBuilder
    {
        private readonly string operationName;

        public HttpRequestSpanBuilder(ISpanBuilder spanBuilder, string operationName)
            : base(spanBuilder)
        {
            this.operationName = operationName;
        }

        public virtual void SetRequestDetails(Uri uri, string httpMethodName, int contentLength)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (httpMethodName == null)
                throw new ArgumentNullException(nameof(httpMethodName));

            //TODO: move to helpers
            var urlWithoutQuery = uri.ToString().Split('?')[0];
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Request.Url, urlWithoutQuery);
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Request.Method, httpMethodName);
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Request.Size, contentLength.ToPrettyString());

            //TODO: use UrlNormalizer
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Operation, operationName ?? $"({httpMethodName.ToUpper()}): {urlWithoutQuery}");
        }

        public virtual void SetResponseDetails(int responseCode, int contentLength)
        {
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Response.Code, responseCode.ToPrettyString());
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Response.Size, contentLength.ToPrettyString());
        }
    }
}