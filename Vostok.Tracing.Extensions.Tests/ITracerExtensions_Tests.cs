using FluentAssertions;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Http;

namespace Vostok.Tracing.Extensions.Tests;

[TestFixture]
internal class ITracerExtensions_Tests
{
    [Test]
    public void CleanCurrentContext_should_work()
    {
        var tracer = new Tracer(new TracerSettings(new DevNullSpanSender()));

        using (tracer.BeginHttpClientSpan())
        {
            var context = tracer.CurrentContext;
            context.Should().NotBeNull();

            using (tracer.CleanCurrentContext())
            {
                tracer.CurrentContext.Should().BeNull();
                
                using (tracer.CleanCurrentContext())
                {
                    tracer.CurrentContext.Should().BeNull();
                }
            }

            tracer.CurrentContext.Should().BeEquivalentTo(context);
        }
    }
}