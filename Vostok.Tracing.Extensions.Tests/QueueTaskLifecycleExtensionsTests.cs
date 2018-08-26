using System;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.TracerExtensions;

namespace Vostok.Tracing.Extensions.Tests
{
    [TestFixture]
    public class QueueTaskLifecycleExtensionsTests
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
        public void BeginQueueTaskLifecycleSpan_should_return_spanbuilder_with_kind_annotation()
        {
            var lifecycleSpanBuilder = tracer.BeginQueueTaskLifecycleSpan();

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.TaskLifecycle);
        }

        [Test]
        public void BeginQueueTaskLifecycleSpan_should_return_spanbuilder_without_operation_annotation_when_was_not_called_SetQueueInfo()
        {
            var lifecycleSpanBuilder = tracer.BeginQueueTaskLifecycleSpan();

            innerSpanBuilder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Operation, Arg.Any<string>());
        }

        [Test]
        public void SetQueueInfo_should_set_queue_info_annotation()
        {
            var lifecycleSpanBuilder = tracer.BeginQueueTaskLifecycleSpan();
            lifecycleSpanBuilder.SetQueueInfo("rabbitmq", "tasks", taskId);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Type, "rabbitmq");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Topic, "tasks");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.TaskId, taskId.ToString());
        }

        [Test]
        public void SetQueueInfo_should_not_set_operation_annotation()
        {
            var producerSpanBuilder = tracer.BeginQueueTaskLifecycleSpan();
            producerSpanBuilder.SetQueueInfo("rabbitmq", "tasks", taskId);

            innerSpanBuilder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Operation, Arg.Any<string>());
        }
    }
}