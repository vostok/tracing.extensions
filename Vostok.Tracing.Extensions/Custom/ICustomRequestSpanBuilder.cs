using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Custom
{
    /// <summary>
    /// Represents a builder that produces annotations relevant for not HTTP request spans of <see cref="WellKnownSpanKinds.Custom.Client"/> kind.
    /// </summary>
    [PublicAPI]
    public interface ICustomRequestSpanBuilder : ICustomSpanBuilder
    {
        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Request.Size"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetRequestDetails([CanBeNull] long? size);

        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description>Custom <see cref="WellKnownAnnotations.Custom.Response.Status"/> (if provided with non-null value)</description></item>
        ///     <item><description>Well-known <see cref="WellKnownAnnotations.Common.Status"/> (if provided with non-null value)</description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Response.Size"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetResponseDetails([CanBeNull] string customStatus, [CanBeNull] string wellKnownStatus, [CanBeNull] long? size);
        
        /// <summary>
        /// <para>Produces annotation with unknown and possibly modified <paramref name="key"/> (should be used only when no suitable <see cref="WellKnownAnnotations"/> is available)</para>
        /// </summary>
        void SetCustomAnnotation([NotNull] string key, [CanBeNull] object value, bool allowOverwrite = true);
    }
}