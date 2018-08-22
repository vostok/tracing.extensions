using System;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    public interface IHttpRequestSpanBuilder : ISpanBuilder
    {
        void SetRequestDetails(Uri uri, string httpMethodName, int contentLength);

        void SetResponseDetails(int responseCode, int contentLength);
    }
}