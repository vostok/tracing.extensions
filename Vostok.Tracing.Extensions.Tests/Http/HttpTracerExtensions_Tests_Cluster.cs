using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.Http;

namespace Vostok.Tracing.Extensions.Tests.Http
{
    [TestFixture]
    internal class HttpTracerExtensions_Tests_Cluster : HttpTracerExtensions_Tests_Base
    {
        [Test]
        public void Should_write_cluster_kind_annotation()
        {
            tracer.BeginHttpClusterSpan();

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Common.Kind, WellKnownSpanKinds.HttpRequest.Cluster);
        }

        [Test]
        public void SetTargetDetails_should_record_target_service_annotation()
        {
            tracer.BeginHttpClusterSpan().SetTargetDetails("srv", "env");

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.TargetService, "srv");
        }

        [Test]
        public void SetTargetDetails_should_record_target_environment_annotation()
        {
            tracer.BeginHttpClusterSpan().SetTargetDetails("srv", "env");

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Request.TargetEnvironment, "env");
        }

        [Test]
        public void SetTargetDetails_should_record_nothing_when_provided_with_null_values()
        {
            tracer.BeginHttpClusterSpan().SetTargetDetails(null, null);

            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Request.TargetService, Arg.Any<string>());
            builder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Http.Request.TargetEnvironment, Arg.Any<string>());
        }

        [Test]
        public void SetClusterStrategy_should_record_strategy_annotation()
        {
            tracer.BeginHttpClusterSpan().SetClusterStrategy("sequential-2");

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Cluster.Strategy, "sequential-2");
        }

        [Test]
        public void SetClusterStrategy_should_record_status_annotation()
        {
            tracer.BeginHttpClusterSpan().SetClusterStatus("success");

            builder.Received(1).SetAnnotation(WellKnownAnnotations.Http.Cluster.Status, "success");
        }

        protected override IHttpRequestSpanBuilder BeginSpan(string operationName = null) =>
            tracer.BeginHttpClusterSpan(operationName);
    }
}