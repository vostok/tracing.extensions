using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Custom
{
    [PublicAPI]
    public static class CustomTracerExtensions
    {
        /// <summary>
        /// <para>Begins a span of <see cref="WellKnownSpanKinds.Custom.Client"/> kind and returns a specialized builder to aid in filling relevant annotations.</para>
        /// <para>See <see cref="ICustomRequestClientSpanBuilder"/> for more details.</para>
        /// </summary>
        public static ICustomRequestClientSpanBuilder BeginCustomRequestClientSpan([NotNull] this ITracer tracer, [NotNull] string operationName)
        {
            if (tracer == null)
                throw new ArgumentNullException(nameof(tracer));

            if (operationName == null)
                throw new ArgumentNullException(nameof(operationName));

            return new CustomRequestClientSpanBuilder(tracer.BeginSpan(), operationName);
        }

        /// <summary>
        /// <para>Begins a span of <see cref="WellKnownSpanKinds.Custom.Client"/> kind and returns a specialized builder to aid in filling relevant annotations.</para>
        /// <para>See <see cref="ICustomRequestClientSpanBuilder"/> for more details.</para>
        /// </summary>
        public static ICustomOperationSpanBuilder BeginCustomOperationSpan([NotNull] this ITracer tracer, [NotNull] string operationName)
        {
            if (tracer == null)
                throw new ArgumentNullException(nameof(tracer));

            if (operationName == null)
                throw new ArgumentNullException(nameof(operationName));

            return new CustomOperationSpanBuilder(tracer.BeginSpan(), operationName);
        }
    }
}