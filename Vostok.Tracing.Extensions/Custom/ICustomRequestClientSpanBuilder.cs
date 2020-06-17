using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Custom
{
    /// <summary>
    /// Represents a builder that produces annotations relevant for not HTTP request spans of <see cref="WellKnownSpanKinds.Custom.Client"/> kind.
    /// </summary>
    [PublicAPI]
    public interface ICustomRequestClientSpanBuilder : ICustomRequestSpanBuilder
    {
        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Request.TargetService"/> (if provided with non-null value)</description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Request.TargetEnvironment"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetTargetDetails([CanBeNull] string targetService, [CanBeNull] string targetEnvironment);

        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Request.Replica"/></description></item>
        /// </list>
        /// </summary>
        void SetReplica([NotNull] string replica);
    }
}