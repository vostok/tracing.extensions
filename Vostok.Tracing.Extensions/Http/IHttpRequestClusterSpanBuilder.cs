using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    /// <summary>
    /// <para>Represents a builder that produces annotations relevant for HTTP request spans of <see cref="WellKnownSpanKinds.HttpRequest.Cluster"/> kind.</para>
    /// <para>See <see cref="IHttpRequestSpanBuilder.SetRequestDetails"/>, <see cref="IHttpRequestSpanBuilder.SetResponseDetails"/>, <see cref="SetTargetDetails"/>, <see cref="SetClusterStrategy"/> and <see cref="SetClusterStatus"/> for more info.</para>
    /// </summary>
    [PublicAPI]
    public interface IHttpRequestClusterSpanBuilder : IHttpRequestSpanBuilder
    {
        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Http.Request.TargetService"/> (if provided with non-null value)</description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Http.Request.TargetEnvironment"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetTargetDetails([CanBeNull] string targetService, [CanBeNull] string targetEnvironment);

        /// <summary>
        /// Produces a <see cref="WellKnownAnnotations.Http.Cluster.Strategy"/> annotation from given <paramref name="strategyName"/> (if provided with non-null value).
        /// </summary>
        void SetClusterStrategy([NotNull] string strategyName);

        /// <summary>
        /// Produces a <see cref="WellKnownAnnotations.Http.Cluster.Status"/> annotation from given <paramref name="status"/> (if provided with non-null value).
        /// </summary>
        void SetClusterStatus([NotNull] string status);
    }
}