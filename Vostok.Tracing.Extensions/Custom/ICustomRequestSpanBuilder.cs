﻿using JetBrains.Annotations;
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
        void SetRequestDetails([CanBeNull] long? bodySize);

        /// <summary>
        /// <para>Produces following annotations based on given inputs:</para>
        /// <list type="bullet">
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Response.Status"/></description></item>
        ///     <item><description><see cref="WellKnownAnnotations.Custom.Response.Size"/> (if provided with non-null value)</description></item>
        /// </list>
        /// </summary>
        void SetResponseDetails([NotNull] string status, [CanBeNull] long? bodySize);
    }
}