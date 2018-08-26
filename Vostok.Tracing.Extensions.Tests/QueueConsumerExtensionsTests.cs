using System;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.TracerExtensions;

namespace Vostok.Tracing.Extensions.Tests
{
    [TestFixture]
    public class QueueConsumerExtensionsTests
    {
        private ITracer tracer;
        private ISpanBuilder innerSpanBuilder;

        private Guid taskId;

        [SetUp]
        public void TestSetup()
        {
            tracer = Substitute.For<ITracer>();
            innerSpanBuilder = Substitute.For<ISpanBuilder>();

            taskId = Guid.NewGuid();

            tracer.BeginSpan().Returns(innerSpanBuilder);
        }

        [Test]
        public void BeginQueueConsumerSpan_should_return_spanbuilder_with_kind_annotation()
        {
            var consumerSpanBuilder = tracer.BeginQueueConsumerSpan();

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.Consumer);
        }

        [Test]
        public void SetExecutionResult_should_set_queue_info_and_operation_annotations()
        {
            var consumerSpanBuilder = tracer.BeginQueueConsumerSpan();
            consumerSpanBuilder.SetExecutionResult("success");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.ExecutionResult, "success");
        }
    }
}