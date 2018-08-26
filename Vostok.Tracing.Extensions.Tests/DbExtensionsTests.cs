using NSubstitute;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;
using Vostok.Tracing.Extensions.TracerExtensions;

namespace Vostok.Tracing.Extensions.Tests
{
    [TestFixture]
    public class DbExtensionsTests
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
        public void BeginDatabaseSpan_should_return_spanbuilder_with_kind_annotation()
        {
            var databaseSpanBuilder = tracer.BeginDatabaseSpan();

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Kind, WellKnownSpanKinds.Database);
        }

        [Test]
        public void BeginDatabaseSpan_should_return_spanbuilder_without_operation_annotation_when_was_not_called_SetDatabaseInfo()
        {
            var databaseSpanBuilder = tracer.BeginDatabaseSpan();

            innerSpanBuilder.DidNotReceive().SetAnnotation(WellKnownAnnotations.Operation, Arg.Any<string>());
        }

        [Test]
        public void SetDatabaseInfo_should_set_dbtype_annotation()
        {
            var databaseSpanBuilder = tracer.BeginDatabaseSpan();
            databaseSpanBuilder.SetDatabaseInfo("mssql");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Database.Type, "mssql");
        }

        [Test]
        public void SetDatabaseInfo_should_set_operation_annotation_with_default_value_when_operation_name_null()
        {
            var databaseSpanBuilder = tracer.BeginDatabaseSpan(null);
            databaseSpanBuilder.SetDatabaseInfo("mssql");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "(mssql) request");
        }

        [Test]
        public void SetDatabaseInfo_should_set_operation_annotation_when_operation_name_given()
        {
            var databaseSpanBuilder = tracer.BeginDatabaseSpan("insert data");
            databaseSpanBuilder.SetDatabaseInfo("mssql");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Operation, "insert data");
        }

        [Test]
        public void SetExecutionResult_should_return_spanbuilder_with_executionresult_annotation()
        {
            var databaseSpanBuilder = tracer.BeginDatabaseSpan();
            databaseSpanBuilder.SetExecutionResult("failed");

            innerSpanBuilder.Received().SetAnnotation(WellKnownAnnotations.Database.ExecutionResult, "failed");
        }
    }
}