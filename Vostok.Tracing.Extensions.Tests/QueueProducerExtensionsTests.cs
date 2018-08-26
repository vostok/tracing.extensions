using System;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.TracerExtensions;

namespace Vostok.Tracing.Extensions.Tests
{
    [TestFixture]
    public class QueueProducerExtensionsTests
    {
        private ITracer tracer;
        private ISpanBuilder innerSpanBuilder;

        [SetUp]
        public void TestSetup()
        {
            tracer = Substitute.For<ITracer>();
            innerSpanBuilder = Substitute.For<ISpanBuilder>();

            tracer.BeginSpan().Returns(innerSpanBuilder);
        }

        [Test]
        public void BeginQueueProducerSpan_should_return_spanbuilder_with_kind_annotation()
        {
            var producerSpanBuilder = tracer.BeginQueueProducerSpan();

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.Producer);
        }

        [Test]
        public void BeginQueueProducerSpan_should_return_spanbuilder_without_operation_annotation_when_was_not_called_SetQueueInfo()
        {
            var producerSpanBuilder = tracer.BeginQueueProducerSpan();

            innerSpanBuilder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Operation, Arg.Any<string>());
        }

        [Test]
        public void SetQueueInfo_should_set_queue_info_annotation_without_taskId_if_taskId_has_not_passed()
        {
            var producerSpanBuilder = tracer.BeginQueueProducerSpan();
            producerSpanBuilder.SetQueueInfo("rabbitmq", "tasks");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Type, "rabbitmq");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Topic, "tasks");
            innerSpanBuilder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Queue.TaskId, Arg.Any<string>());
        }

        [Test]
        public void SetQueueInfo_should_set_queue_info_annotation_with_taskId_when_taskId_passed()
        {
            var taskId = Guid.NewGuid();
            var producerSpanBuilder = tracer.BeginQueueProducerSpan();
            producerSpanBuilder.SetQueueInfo("rabbitmq", "tasks", taskId);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Type, "rabbitmq");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Topic, "tasks");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.TaskId, taskId.ToString());
        }

        [Test]
        public void SetQueueInfo_should_set_operation_annotation_with_default_value_when_operation_name_null()
        {
            var producerSpanBuilder = tracer.BeginQueueProducerSpan(null);
            producerSpanBuilder.SetQueueInfo("rabbitmq", "tasks");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "(rabbitmq) put to [tasks]");
        }

        [Test]
        public void SetDatabaseInfo_should_set_operation_annotation_when_operation_name_given()
        {
            var producerSpanBuilder = tracer.BeginQueueProducerSpan("insert task to rabbitmq");
            producerSpanBuilder.SetQueueInfo("rabbitmq", "tasks");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "insert task to rabbitmq");
        }

        [Test]
        public void SetActionResult_should_return_spanbuilder_with_actionresult_annotations()
        {
            var traceId = Guid.NewGuid();
            var producerSpanBuilder = tracer.BeginQueueProducerSpan();
            producerSpanBuilder.SetActionResult("success", traceId);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.ActionResult, "success");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.TaskTraceId, traceId.ToString());
            innerSpanBuilder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Queue.TaskId, Arg.Any<string>());
        }

        [Test]
        public void SetActionResult_should_return_spanbuilder_with_actionresult_and_taskId_annotations_when_tasid_passed()
        {
            var traceId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            var producerSpanBuilder = tracer.BeginQueueProducerSpan();
            producerSpanBuilder.SetActionResult("success", traceId, taskId);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.ActionResult, "success");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.TaskTraceId, traceId.ToString());
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.TaskId, taskId.ToString());
        }
    }
}