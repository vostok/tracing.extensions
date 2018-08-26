using System;
using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Abstractions;

namespace Vostok.Tracing.Extensions.SpanBuilders
{
    internal class DatabaseSpanBuilder : InternalSpanBuilder, IDatabaseSpanBuilder
    {
        private readonly string operationName;

        public DatabaseSpanBuilder(ISpanBuilder spanBuilder, string operationName)
            : base(spanBuilder)
        {
            this.operationName = operationName;
        }

        public void SetDatabaseInfo([NotNull] string type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Database.Type, type);

            SpanBuilder.SetAnnotation(WellKnownAnnotations.Operation, operationName ?? $"({type}) request");
        }

        public void SetExecutionResult([NotNull] string executionResult)
        {
            if (executionResult == null)
                throw new ArgumentNullException(nameof(executionResult));
            SpanBuilder.SetAnnotation(WellKnownAnnotations.Database.ExecutionResult, executionResult);
        }
    }
}