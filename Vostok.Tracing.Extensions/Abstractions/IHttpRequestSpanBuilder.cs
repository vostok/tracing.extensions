using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IHttpRequestSpanBuilder : ISpanBuilder
    {
        void SetRequestDetails([NotNull] Uri uri, [NotNull] string httpMethodName, int contentLength);

        void SetResponseDetails(int responseCode, int contentLength);
    }
}