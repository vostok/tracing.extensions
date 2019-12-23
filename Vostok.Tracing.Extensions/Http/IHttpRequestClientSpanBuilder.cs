using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    /// <summary>
    /// <para>Represents a builder that produces annotations relevant for HTTP request spans of <see cref="WellKnownSpanKinds.HttpRequest.Client"/> kind.</para>
    /// <para>See <see cref="IHttpRequestSpanBuilder.SetRequestDetails(System.Uri,string,System.Nullable{long})"/>, <see cref="IHttpRequestSpanBuilder.SetResponseDetails"/> and <see cref="SetTargetDetails"/> for more info.</para>
    /// </summary>
    [PublicAPI]
    public interface IHttpRequestClientSpanBuilder : IHttpRequestSpanBuilder
    {
        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Http.Request.TargetService"/> (if provided with non-null value)</description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Http.Request.TargetEnvironment"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetTargetDetails([CanBeNull] string targetService, [CanBeNull] string targetEnvironment);
    }
}