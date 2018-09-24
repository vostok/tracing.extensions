using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Http
{
    [PublicAPI]
    public static class HttpTracerExtensions
    {
        /// <summary>
        /// <para>Begins a span of <see cref="WellKnownSpanKinds.HttpRequest.Client"/> kind and returns a specialized builder to aid in filling relevant annotations.</para>
        /// <para>See <see cref="IHttpRequestClientSpanBuilder"/> for more details.</para>
        /// <para>If provided <paramref name="operationName"/> is <c>null</c>, <see cref="WellKnownAnnotations.Common.Operation"/> annotation will be inferred from request properties.</para>
        /// </summary>
        public static IHttpRequestClientSpanBuilder BeginHttpClientSpan([NotNull] this ITracer tracer, [CanBeNull] string operationName = null)
        {
            if (tracer == null)
                throw new ArgumentNullException(nameof(tracer));

            return new HttpRequestClientSpanBuilder(tracer.BeginSpan(), operationName);
        }

        /// <summary>
        /// <para>Begins a span of <see cref="WellKnownSpanKinds.HttpRequest.Server"/> kind and returns a specialized builder to aid in filling relevant annotations.</para>
        /// <para>See <see cref="IHttpRequestServerSpanBuilder"/> for more details.</para>
        /// <para>If provided <paramref name="operationName"/> is <c>null</c>, <see cref="WellKnownAnnotations.Common.Operation"/> annotation will be inferred from request properties.</para>
        /// </summary>
        public static IHttpRequestServerSpanBuilder BeginHttpServerSpan([NotNull] this ITracer tracer, [CanBeNull] string operationName = null)
        {
            if (tracer == null)
                throw new ArgumentNullException(nameof(tracer));

            return new HttpRequestServerSpanBuilder(tracer.BeginSpan(), operationName);
        }

        /// <summary>
        /// <para>Begins a span of <see cref="WellKnownSpanKinds.HttpRequest.Cluster"/> kind and returns a specialized builder to aid in filling relevant annotations.</para>
        /// <para>See <see cref="IHttpRequestClusterSpanBuilder"/> for more details.</para>
        /// <para>If provided <paramref name="operationName"/> is <c>null</c>, <see cref="WellKnownAnnotations.Common.Operation"/> annotation will be inferred from request properties.</para>
        /// </summary>
        public static IHttpRequestClusterSpanBuilder BeginHttpClusterSpan([NotNull] this ITracer tracer, [CanBeNull] string operationName = null)
        {
            if (tracer == null)
                throw new ArgumentNullException(nameof(tracer));

            return new HttpRequestClusterSpanBuilder(tracer.BeginSpan(), operationName);
        }
    }
}