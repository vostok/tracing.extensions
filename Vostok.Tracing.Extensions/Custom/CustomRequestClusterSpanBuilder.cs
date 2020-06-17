using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Custom
{
    internal class CustomRequestClusterSpanBuilder : CustomRequestSpanBuilder, ICustomRequestClusterSpanBuilder
    {
        public CustomRequestClusterSpanBuilder([NotNull] ISpanBuilder builder, [NotNull] string operationName)
            : base(builder, WellKnownSpanKinds.Custom.Cluster, operationName)
        {
        }
    }
}