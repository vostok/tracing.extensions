using JetBrains.Annotations;

namespace Vostok.Tracing.Extensions.Custom
{
    [PublicAPI]
    public static class ICustomSpanBuilderExtensions
    {
        public static void SetCustomAnnotation(this ICustomSpanBuilder builder, string key, object value, bool allowOverwrite = true) =>
            builder.SetAnnotation($"custom.{key}", value, allowOverwrite);
    }
}