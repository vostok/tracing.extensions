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
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Operation.Status"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetOperationStatus([NotNull] string status);
    }
}