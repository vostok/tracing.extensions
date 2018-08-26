using System;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.TracerExtensions;

namespace Vostok.Tracing.Extensions.Tests
{
    [TestFixture]
    public class QueueTaskLifecycleEventExtensionsTests
    {
        private ITracer tracer;
        private ISpanBuilder innerSpanBuilder;

        private Guid traceId;
        private Guid spanId;

        [SetUp]
        public void TestSetup()
        {
            tracer = Substitute.For<ITracer>();
            innerSpanBuilder = Substitute.For<ISpanBuilder>();
            traceId = Guid.NewGuid();
            spanId = Guid.NewGuid();

            tracer.BeginSpan().Returns(innerSpanBuilder);
        }

        [Test]
        public void BeginQueueTaskLifecycleEventSpan_should_return_spanbuilder_with_kind_annotation()
        {
            var lifecycleEventSpanBuilder = tracer.BeginQueueTaskLifecycleEventSpan("pass task to consumer");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Queue.TaskLifecycleEvent);
        }

        [Test]
        public void SetSourceInfo_should_set_source_info_and_operation_annotations()
        {
            var lifecycleEventSpanBuilder = tracer.BeginQueueTaskLifecycleEventSpan("pass task to consumer");
            lifecycleEventSpanBuilder.SetSourceInfo(traceId, spanId);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Source.TraceId, traceId.ToString());
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Queue.Source.SpanId, spanId.ToString());
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "pass task to consumer");
        }
    }
}