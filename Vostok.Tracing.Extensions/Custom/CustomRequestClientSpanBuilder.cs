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

        public void SetTargetDetails(string targetService, string targetEnvironment)
        {
            if (targetService != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Request.TargetService, targetService);
            }

            if (targetEnvironment != null)
            {
                SetAnnotation(WellKnownAnnotations.Http.Request.TargetEnvironment, targetEnvironment);
            }
        }
    }
}