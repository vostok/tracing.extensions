using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Custom
{
    internal class CustomRequestClientSpanBuilder : CustomRequestSpanBuilder, ICustomRequestClientSpanBuilder
    {
        public CustomRequestClientSpanBuilder([NotNull] ISpanBuilder builder, [NotNull] string operationName)
            : base(builder, WellKnownSpanKinds.Custom.Client, operationName)
        {
        }

        public void SetReplica(string replica) =>
            SetAnnotation(WellKnownAnnotations.Custom.Request.Replica, replica);
    }
}