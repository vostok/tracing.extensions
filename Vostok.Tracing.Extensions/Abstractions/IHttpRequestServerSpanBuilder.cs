using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IHttpRequestServerSpanBuilder : IHttpRequestSpanBuilder
    {
        void SetClientDetails([NotNull] string name, [NotNull] string address);
    }
}