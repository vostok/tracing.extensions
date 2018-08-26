using System;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class InternalSpanBuilder : ISpanBuilder
    {
        protected readonly ISpanBuilder SpanBuilder;

        public InternalSpanBuilder(ISpanBuilder spanBuilder)
        {
            SpanBuilder = spanBuilder;
        }

        public bool IsEndless
        {
            get => SpanBuilder.IsEndless;
            set => SpanBuilder.IsEndless = value;
        }

        public void Dispose()
        {
            SpanBuilder.Dispose();
        }

        public void SetAnnotation(string key, string value, bool allowOverwrite = true)
        {
            SpanBuilder.SetAnnotation(key, value, allowOverwrite);
        }

        public void SetBeginTimestamp(DateTimeOffset timestamp)
        {
            SpanBuilder.SetBeginTimestamp(timestamp);
        }

        public void SetEndTimestamp(DateTimeOffset timestamp)
        {
            SpanBuilder.SetEndTimestamp(timestamp);
        }
    }
}