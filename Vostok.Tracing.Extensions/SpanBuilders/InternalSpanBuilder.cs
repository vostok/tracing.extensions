using System;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class InternalSpanBuilder : ISpanBuilder
    {
        protected readonly ISpanBuilder spanBuilder;

        public InternalSpanBuilder(ISpanBuilder spanBuilder)
        {
            this.spanBuilder = spanBuilder;
        }

        public bool IsEndless
        {
            get => spanBuilder.IsEndless;
            set => spanBuilder.IsEndless = value;
        }

        public void Dispose()
        {
            spanBuilder.Dispose();
        }

        public void SetAnnotation(string key, string value, bool allowOverwrite = true)
        {
            spanBuilder.SetAnnotation(key, value, allowOverwrite);
        }

        public void SetBeginTimestamp(DateTimeOffset timestamp)
        {
            spanBuilder.SetBeginTimestamp(timestamp);
        }

        public void SetEndTimestamp(DateTimeOffset timestamp)
        {
            spanBuilder.SetEndTimestamp(timestamp);
        }
    }
}