using System;
using System.Globalization;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Helpers
{
    internal class SpanBuilderDecorator : ISpanBuilder
    {
        private readonly ISpanBuilder builder;
        private readonly string operationName;

        public SpanBuilderDecorator([NotNull] ISpanBuilder builder, [NotNull] string kind, [CanBeNull] string operationName)
        {
            this.builder = builder;
            this.operationName = operationName;

            builder.SetAnnotation(WellKnownAnnotations.Common.Kind, kind);
        }

        public ISpan CurrentSpan => builder.CurrentSpan;

        public void Dispose()
        {
            if (operationName != null && !CurrentSpan.Annotations.ContainsKey(WellKnownAnnotations.Common.Operation))
            {
                SetAnnotation(WellKnownAnnotations.Common.Operation, operationName);
            }

            builder.Dispose();
        }

        public void SetAnnotation(string key, int value)
        {
            SetAnnotation(key, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetAnnotation(string key, long value)
        {
            SetAnnotation(key, value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetAnnotation(string key, string value, bool allowOverwrite = true)
        {
            builder.SetAnnotation(key, value, allowOverwrite);
        }

        public void SetBeginTimestamp(DateTimeOffset timestamp)
        {
            builder.SetBeginTimestamp(timestamp);
        }

        public void SetEndTimestamp(DateTimeOffset? timestamp)
        {
            builder.SetEndTimestamp(timestamp);
        }
    }
}
