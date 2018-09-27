using System.Net;
using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Http;

namespace Vostok.Tracing.Extensions.Tests.Http
{
    [TestFixture]
    internal class HttpTracerExtensions_Tests_Server : HttpTracerExtensions_Tests_Base
    {
        [Test]
        public void Should_write_server_kind_annotation()
        {
            tracer.BeginHttpServerSpan();

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Common.Kind, WellKnownSpanKinds.HttpRequest.Server);
        }

        [Test]
        public void SetClientDetails_should_record_client_name_annotation()
        {
            tracer.BeginHttpServerSpan().SetClientDetails("srv", IPAddress.Parse("1.2.3.4"));

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Client.Name, "srv");
        }

        [Test]
        public void SetClientDetails_should_record_client_address_annotation()
        {
            var address = IPAddress.Parse("1.2.3.4");

            tracer.BeginHttpServerSpan().SetClientDetails("srv", address);

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Client.Address, address);
        }

        [Test]
        public void SetClientDetails_should_record_nothing_when_provided_with_null_values()
        {
            tracer.BeginHttpServerSpan().SetClientDetails(null, null);

            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Client.Name, Arg.Any<string>());
            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Client.Address, Arg.Any<string>());
        }

        protected override IHttpRequestSpanBuilder BeginSpan(string operationName = null) =>
            tracer.BeginHttpServerSpan(operationName);
    }
}