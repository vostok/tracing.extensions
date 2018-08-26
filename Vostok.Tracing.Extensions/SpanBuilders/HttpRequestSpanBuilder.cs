using System;
using Vostok.Commons.Helpers;
using Vostok.Commons.Helpers.Extensions;
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

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Request.Url, uri.ToStringWithoutQuery());
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Request.Method, httpMethodName);
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Request.Size, contentLength.ToPrettyString());

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Operation, operationName ?? $"({httpMethodName.ToUpper()}): {UrlNormalizer.NormalizePath(uri)}");
        }

        public virtual void SetResponseDetails(int responseCode, int contentLength)
        {
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Response.Code, responseCode.ToPrettyString());
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Http.Response.Size, contentLength.ToPrettyString());
        }
    }
}