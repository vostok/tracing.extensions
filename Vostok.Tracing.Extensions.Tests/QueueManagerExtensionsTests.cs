using System;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.TracerExtensions;

namespace Vostok.Tracing.Extensions.Tests
{
    [TestFixture]
    public class QueueManagerExtensionsTests
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
        public void BeginQueueManagerEventSpan_should_return_spanbuilder_with_kind_annotation()
        {
            var managerSpanBuilder = tracer.BeginQueueManagerEventSpan("prolong task");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.Manager);
        }

        [Test]
        public void SetQueueInfo_should_set_queue_info_and_operation_annotations()
        {
            var managerSpanBuilder = tracer.BeginQueueManagerEventSpan("prolong task");
            managerSpanBuilder.SetQueueInfo("rabbitmq", "tasks", taskId);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Type, "rabbitmq");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Topic, "tasks");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.TaskId, taskId.ToString());
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "prolong task");
        }

        [Test]
        public void SetActionResult_should_return_spanbuilder_with_actionresult_annotation()
        {
            var managerSpanBuilder = tracer.BeginQueueManagerEventSpan("prolong task");
            managerSpanBuilder.SetActionResult("success");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.ActionResult, "success");
        }
    }
}