using System;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Http;

namespace Vostok.Tracing.Extensions.Tests.Http
{
    internal abstract class HttpTracerExtensions_Tests_Base
    {
        protected ITracer tracer;
        protected ISpanBuilder builder;

        private Uri absoluteUrl;
        private Uri relativeUrl;

        [SetUp]
        public void TestSetup()
        {
            tracer = Substitute.For<ITracer>();
            tracer.BeginSpan().Returns(builder = Substitute.For<ISpanBuilder>());

            absoluteUrl = new Uri("https://vostok.tools/version/123?q=a", UriKind.Absolute);
            relativeUrl = new Uri("version/123?q=a", UriKind.Relative);
        }

        [Test]
        public void Should_write_provided_operation_name_on_dispose_if_nothing_else_was_called()
        {
            var specializedBuilder = BeginSpan("op");

            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Common.Operation, Arg.Any<string>());

            specializedBuilder.Dispose();

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Common.Operation, "op");
        }

        [Test]
        public void Should_not_write_an_operation_name_on_dispose_if_none_was_provided_and_nothing_else_was_called()
        {
            BeginSpan().Dispose();

            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Common.Operation, Arg.Any<string>());
        }

        [Test]
        public void Should_write_provided_operation_name_on_SetRequestDetails_call()
        {
            BeginSpan("op").SetRequestDetails(absoluteUrl, "GET", null);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Common.Operation, Arg.Any<string>());
            builder.Received(1).SetAnnotation(WellKnownAnnotations.Common.Operation, "op");
        }

        [Test]
        public void Should_write_inferred_operation_name_on_SetRequestDetails_call_if_none_was_provided_expicitly()
        {
            BeginSpan().SetRequestDetails(absoluteUrl, "GET", null);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Common.Operation, "GET: version/~");
        }

        [Test]
        public void SetRequestDetails_should_record_url_annotation_for_absolute_urls_without_query_and_normalization()
        {
            BeginSpan().SetRequestDetails(absoluteUrl, "GET", null);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.Url, "https://vostok.tools/version/123");
        }

        [Test]
        public void SetRequestDetails_should_record_url_annotation_for_relative_urls_without_query_and_normalization()
        {
            BeginSpan().SetRequestDetails(relativeUrl, "GET", null);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.Url, "version/123");
        }

        [Test]
        public void SetRequestDetails_should_record_method_annotation()
        {
            BeginSpan().SetRequestDetails(absoluteUrl, "GET", null);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.Method, "GET");
        }

        [Test]
        public void SetRequestDetails_should_record_size_annotation_when_provided_with_a_value()
        {
            BeginSpan().SetRequestDetails(absoluteUrl, "GET", 456);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.Size, "456");
        }

        [Test]
        public void SetRequestDetails_should_not_record_size_annotation_when_no_value_is_given()
        {
            BeginSpan().SetRequestDetails(absoluteUrl, "GET", null);

            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Request.Size, Arg.Any<string>());
        }

        [Test]
        public void SetResponseDetails_should_record_code_annotation()
        {
            BeginSpan().SetResponseDetails(200, null);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Response.Code, "200");
        }

        [Test]
        public void SetResponseDetails_should_record_size_annotation_when_provided_with_a_value()
        {
            BeginSpan().SetResponseDetails(200, 456);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Response.Size, "456");
        }

        [Test]
        public void SetResponseDetails_should_not_record_size_annotation_when_no_value_is_given()
        {
            BeginSpan().SetResponseDetails(200, null);

            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Response.Size, Arg.Any<string>());
        }

        [Test]
        public void Returned_builder_should_dispose_base_builder_when_disposing()
        {
            BeginSpan().Dispose();

            builder.Received().Dispose();
        }

        [Test]
        public void Returned_builder_should_delegate_SetBeginTimestamp_call_to_base_builder()
        {
            var timestamp = DateTimeOffset.UtcNow;

            BeginSpan().SetBeginTimestamp(timestamp);

            builder.Received().SetBeginTimestamp(timestamp);
        }

        [Test]
        public void Returned_builder_should_delegate_SetEndTimestamp_call_to_base_builder()
        {
            var timestamp = DateTimeOffset.UtcNow;

            BeginSpan().SetEndTimestamp(timestamp);

            builder.Received().SetEndTimestamp(timestamp);
        }

        [Test]
        public void Returned_builder_should_delegate_SetAnnotation_call_to_base_builder()
        {
            BeginSpan().SetAnnotation("k", "v", false);

            builder.Received().SetAnnotation("k", "v", false);
        }

        protected abstract IHttpRequestSpanBuilder BeginSpan(string operationName = null);
    }
}