using System;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.TracerExtensions;

namespace Vostok.Tracing.Extensions.Tests
{
    [TestFixture]
    public class HttpRequestClientExtensionsTests
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
        public void BeginHttpRequestClientSpan_should_return_spanbuilder_with_kind_annotation()
        {
            var clientSpanBuilder = tracer.BeginHttpRequestClientSpan();

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.HttpRequest.Client);
        }

        [Test]
        public void BeginHttpRequestClientSpan_should_return_spanbuilder_without_operation_annotation_when_was_not_called_SetRequestDetails()
        {
            var clientSpanBuilder = tracer.BeginHttpRequestClientSpan();

            innerSpanBuilder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Operation, Arg.Any<string>());
        }

        [Test]
        public void SetRequestDetails_should_set_request_details_annotations()
        {
            const string url = "https://kontur.ru/segment1/segment2?param1=a&param2=b";

            var clientSpanBuilder = tracer.BeginHttpRequestClientSpan();
            clientSpanBuilder.SetRequestDetails(new Uri(url), "GET", 100500);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Http.Request.Url, "https://kontur.ru/segment1/segment2");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Http.Request.Method, "GET");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Http.Request.Size, "100500");
        }

        [Test]
        public void SetRequestDetails_should_set_operation_annotation_with_default_value_when_operation_name_null()
        {
            const string url = "https://kontur.ru/segment1/segment2?param1=a&param2=b";

            var clientSpanBuilder = tracer.BeginHttpRequestClientSpan(null);
            clientSpanBuilder.SetRequestDetails(new Uri(url), "GET", 100500);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "(GET): https://kontur.ru/segment1/segment2");
        }

        [Test]
        public void SetRequestDetails_should_set_operation_annotation_when_operation_name_given()
        {
            const string url = "https://kontur.ru/segment1/segment2?param1=a&param2=b";

            var clientSpanBuilder = tracer.BeginHttpRequestClientSpan("make http request");
            clientSpanBuilder.SetRequestDetails(new Uri(url), "GET", 100500);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "make http request");
        }

        [Test]
        public void SetResponseDetails_should_set_response_details_annotations()
        {
            var clientSpanBuilder = tracer.BeginHttpRequestClientSpan();
            clientSpanBuilder.SetResponseDetails(200, 100501);

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Http.Response.Code, "200");
            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Http.Response.Size, "100501");
        }
    }
}