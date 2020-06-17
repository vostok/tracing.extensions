using JetBrains.Annotations;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Custom
{
    /// <summary>
    /// Represents a builder that produces annotations relevant for custom operations of one of <see cref="WellKnownSpanKinds.Custom"/> kinds.
    /// </summary>
    [PublicAPI]
    public interface ICustomSpanBuilder : ISpanBuilder
    {
    }
}