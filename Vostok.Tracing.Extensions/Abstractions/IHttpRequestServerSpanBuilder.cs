using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    public interface IHttpRequestServerSpanBuilder : IHttpRequestSpanBuilder
    {
        void SetClientDetails(string name, string address);
    }
}