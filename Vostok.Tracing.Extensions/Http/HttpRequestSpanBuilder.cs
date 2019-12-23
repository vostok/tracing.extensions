using System;
using Vostok.Commons.Helpers.Url;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Helpers;

namespace Vostok.Tracing.Extensions.Http
{
    internal abstract class HttpRequestSpanBuilder : SpanBuilderDecorator, IHttpRequestSpanBuilder
    {
        private readonly string operationName;

        protected HttpRequestSpanBuilder(ISpanBuilder builder, string kind, string operationName)
            : base(builder, kind, operationName)
        {
            this.operationName = operationName;
        }

        public void SetRequestDetails(Uri url, string method, long? bodySize)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            if (method == null)
                throw new ArgumentNullException(nameof(method));

            SetAnnotation(WellKnownAnnotations.Common.Operation, operationName ?? $"{method}: {UrlNormalizer.NormalizePath(url)}");

            SetAnnotation(WellKnownAnnotations.Http.Request.Url, url.ToStringWithoutQuery());
            SetAnnotation(WellKnownAnnotations.Http.Request.Method, method);

            if (bodySize.HasValue)
                SetAnnotation(WellKnownAnnotations.Http.Request.Size, bodySize.Value);
        }

        public void SetRequestDetails(string url, string method, long? bodySize)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            if (method == null)
                throw new ArgumentNullException(nameof(method));

            url = UrlExtensions.ToStringWithoutQuery(url);

            SetAnnotation(WellKnownAnnotations.Common.Operation, operationName ?? $"{method}: {UrlNormalizer.NormalizePath(url)}");

            SetAnnotation(WellKnownAnnotations.Http.Request.Url, url);
            SetAnnotation(WellKnownAnnotations.Http.Request.Method, method);

            if (bodySize.HasValue)
                SetAnnotation(WellKnownAnnotations.Http.Request.Size, bodySize.Value);
        }

        public void SetResponseDetails(int responseCode, long? bodySize)
        {
            SetAnnotation(WellKnownAnnotations.Http.Response.Code, responseCode);

            if (bodySize.HasValue)
            {
                SetAnnotation(WellKnownAnnotations.Http.Response.Size, bodySize.Value);
            }
        }
    }
}