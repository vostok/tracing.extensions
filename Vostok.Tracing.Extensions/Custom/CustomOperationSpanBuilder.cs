using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Helpers;

namespace Vostok.Tracing.Extensions.Custom
{
    internal class CustomOperationSpanBuilder : SpanBuilderDecorator, ICustomOperationSpanBuilder
    {
        public CustomOperationSpanBuilder([NotNull] ISpanBuilder builder, [NotNull] string operationName)
            : base(builder, WellKnownSpanKinds.Custom.Operation, operationName)
        {
        }

        public void SetOperationDetails(long? size)
        {
            if (size.HasValue)
                SetAnnotation(WellKnownAnnotations.Custom.Operation.Size, size.Value);
        }

        public void SetOperationStatus(string status)
        {
            SetAnnotation(WellKnownAnnotations.Custom.Operation.Status, status);
        }
    }
}