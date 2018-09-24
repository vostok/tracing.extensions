using System.Net;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    /// <summary>
    /// <para>Represents a builder that produces annotations relevant for HTTP request spans of <see cref="WellKnownSpanKinds.HttpRequest.Server"/> kind.</para>
    /// <para>See <see cref="IHttpRequestSpanBuilder.SetRequestDetails"/>, <see cref="IHttpRequestSpanBuilder.SetResponseDetails"/> and <see cref="SetClientDetails"/> for more info.</para>
    /// </summary>
    [PublicAPI]
    public interface IHttpRequestServerSpanBuilder : IHttpRequestSpanBuilder
    {
        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Http.Client.Name"/> (if provided with non-null value)</description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Http.Client.Address"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetClientDetails([CanBeNull] string name, [CanBeNull] IPAddress address);
    }
}