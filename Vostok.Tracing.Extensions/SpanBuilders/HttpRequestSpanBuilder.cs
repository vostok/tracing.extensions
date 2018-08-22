using System;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions
{
    //implement ISpanBuilder for every span kind?
    internal class HttpRequestSpanBuilder : IHttpRequestSpanBuilder
    {
        protected readonly ISpanBuilder spanBuilder;

        public HttpRequestSpanBuilder(ISpanBuilder spanBuilder)
        {
            this.spanBuilder = spanBuilder;
        }

        public void SetRequestDetails(Uri uri, string httpMethodName, int contentLength)
        {
            spanBuilder.SetAnnotation(AnnotationNames.Http.Request.Url, uri.ToString());
            spanBuilder.SetAnnotation(AnnotationNames.Http.Request.Method, httpMethodName);
            spanBuilder.SetAnnotation(AnnotationNames.Http.Request.ContentLength, contentLength.ToString());

            spanBuilder.SetAnnotation(AnnotationNames.Operation, $"({httpMethodName.ToUpper()}): {NormilizeUrl(uri)}");
        }

        public void SetResponseDetails(int responseCode, int contentLength)
        {
            spanBuilder.SetAnnotation(AnnotationNames.Http.Response.StatusCode, responseCode.ToString());
            spanBuilder.SetAnnotation(AnnotationNames.Http.Response.ContentLength, contentLength.ToString());
        }

        public void Dispose()
        {
            spanBuilder.Dispose();
        }

        public void SetAnnotation(string key, string value, bool allowOverwrite = true)
        {
            spanBuilder.SetAnnotation(key, value, allowOverwrite);
        }

        public void SetBeginTimestamp(DateTimeOffset timestamp)
        {
            spanBuilder.SetBeginTimestamp(timestamp);
        }

        public void SetEndTimestamp(DateTimeOffset timestamp)
        {
            spanBuilder.SetEndTimestamp(timestamp);
        }

        public bool IsEndless
        {
            get => spanBuilder.IsEndless;
            set => spanBuilder.IsEndless = value;
        }

        private string NormilizeUrl(Uri uri)
        {
            //update after transfer normilizer
            return uri.ToString();
        }
    }
}