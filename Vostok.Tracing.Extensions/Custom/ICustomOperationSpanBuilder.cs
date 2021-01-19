using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Custom
{
    /// <summary>
    /// Represents a builder that produces annotations relevant for server operation spans of <see cref="WellKnownSpanKinds.Custom.Operation"/> kind.
    /// </summary>
    [PublicAPI]
    public interface ICustomOperationSpanBuilder : ICustomSpanBuilder
    {
        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Operation.Size"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetOperationDetails([CanBeNull] long? size);

        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item>Custom <description><see cref="WellKnownAnnotations.Custom.Operation.Status"/> (if provided with non-null value)</description></item>
        ///     <item>Well-known <description><see cref="WellKnownAnnotations.Common.Status"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetOperationStatus([CanBeNull] string customStatus, [CanBeNull] string wellKnownStatus);

        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Operation.TargetService"/> (if provided with non-null value)</description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Operation.TargetEnvironment"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetTargetDetails([CanBeNull] string targetService, [CanBeNull] string targetEnvironment);

        /// <summary>
        /// <para>Produces annotation with unknown and possibly modified <paramref name="key"/> (should be used only when no suitable <see cref="WellKnownAnnotations"/> is available)</para>
        /// </summary>
        void SetCustomAnnotation([NotNull] string key, [CanBeNull] object value, bool allowOverwrite = true);
    }
}