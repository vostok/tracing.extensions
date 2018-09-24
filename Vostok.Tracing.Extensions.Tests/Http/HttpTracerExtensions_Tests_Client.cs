using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Http;

namespace Vostok.Tracing.Extensions.Tests.Http
{
    [TestFixture]
    internal class HttpTracerExtensions_Tests_Client : HttpTracerExtensions_Tests_Base
    {
        [Test]
        public void Should_write_client_kind_annotation()
        {
            tracer.BeginHttpClientSpan();

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Common.Kind, WellKnownSpanKinds.HttpRequest.Client);
        }

        [Test]
        public void SetTargetDetails_should_record_target_service_annotation()
        {
            tracer.BeginHttpClientSpan().SetTargetDetails("srv", "env");

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.TargetService, "srv");
        }

        [Test]
        public void SetTargetDetails_should_record_target_environment_annotation()
        {
            tracer.BeginHttpClientSpan().SetTargetDetails("srv", "env");

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.TargetEnvironment, "env");
        }

        [Test]
        public void SetTargetDetails_should_record_nothing_when_provided_with_null_values()
        {
            tracer.BeginHttpClientSpan().SetTargetDetails(null, null);

            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Request.TargetService, Arg.Any<string>());
            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Request.TargetEnvironment, Arg.Any<string>());
        }

        protected override IHttpRequestSpanBuilder BeginSpan(string operationName = null) => 
            tracer.BeginHttpClientSpan(operationName);
    }
}