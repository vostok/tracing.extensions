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

        public void SetRequestDetails(long? bodySize)
        {
            if (bodySize.HasValue)
                SetAnnotation(WellKnownAnnotations.Custom.Request.Size, bodySize.Value);
        }

        public void SetResponseDetails(string status, long? bodySize)
        {
            SetAnnotation(WellKnownAnnotations.Custom.Response.Status, status);

            if (bodySize.HasValue)
                SetAnnotation(WellKnownAnnotations.Custom.Response.Size, bodySize.Value);
        }
    }
}