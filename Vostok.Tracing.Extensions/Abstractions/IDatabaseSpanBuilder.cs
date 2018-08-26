using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Abstractions
{
    [PublicAPI]
    public interface IDatabaseSpanBuilder : ISpanBuilder
    {
        void SetDatabaseInfo([NotNull] string type);

        void SetExecutionResult([NotNull] string executionResult);
    }
}