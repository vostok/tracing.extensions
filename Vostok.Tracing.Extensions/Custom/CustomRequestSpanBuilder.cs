using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Helpers;

namespace Vostok.Tracing.Extensions.Custom
{
    internal class CustomRequestSpanBuilder : SpanBuilderDecorator, ICustomRequestSpanBuilder
    {
        public CustomRequestSpanBuilder([NotNull] ISpanBuilder builder, [NotNull] string kind, [NotNull] string operationName)
            : base(builder, kind, operationName)
        {
        }

        public void SetRequestDetails(long? size)
        {
            if (size.HasValue)
                SetAnnotation(WellKnownAnnotations.Custom.Request.Size, size.Value);
        }

        public void SetResponseDetails(string status, long? size)
        {
            SetAnnotation(WellKnownAnnotations.Custom.Response.Status, status);

            if (size.HasValue)
                SetAnnotation(WellKnownAnnotations.Custom.Response.Size, size.Value);
        }
    }
}