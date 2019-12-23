using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    /// <summary>
    /// <para>Represents a builder that produces annotations relevant for HTTP request spans of any kind.</para>
    /// <para>See <see cref="SetRequestDetails(Uri,string,System.Nullable{long})"/> and <see cref="SetResponseDetails"/> for more info.</para>
    /// </summary>
    [PublicAPI]
    public interface IHttpRequestSpanBuilder : ISpanBuilder
    {
        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Http.Request.Url"/></description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Http.Request.Method"/></description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Http.Request.Size"/> (if provided with non-null value)</description></item>
        /// </list>
        /// <para>Also infers the value for <see cref="WellKnownAnnotations.Common.Operation"/> annotation if no value was provided to an extension method from <see cref="HttpTracerExtensions"/>.</para>
        /// </summary>
        void SetRequestDetails([NotNull] Uri url, [NotNull] string method, [CanBeNull] long? bodySize);

        /// <inheritdoc cref="SetRequestDetails(System.Uri,string,System.Nullable{long})"/>
        void SetRequestDetails([NotNull] string url, [NotNull] string method, [CanBeNull] long? bodySize);

        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Http.Response.Code"/></description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Http.Response.Size"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetResponseDetails(int responseCode, [CanBeNull] long? bodySize);
    }
}