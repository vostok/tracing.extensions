using System;
using NUnit.Framework;
using Vostok.Tracing.Abstractions;

namespace Vostok.Tracing.Extensions.Tests
{
    public class HttpRequestExtensions_Tests
    {
        private ITracer tracer;

        public void SetUp()
        {
            tracer = new Tracer();
        }

        public void HttpRequestClient_Example()
        {
            using (var span = tracer.BeginHttpRequestClientSpan())
            {
                span.SetRequestDetails(new Uri("from request info"), "GET", 120323);

                //do request

                span.SetResponseDetails(200, 43453423);
            }
        }

        public void HttpRequestServer_Example()
        {
            using (var span = tracer.BeginHttpRequestServerSpan())
            {
                span.SetRequestDetails(new Uri("current url"), "GET", 120323);
                span.SetClientDetails("bills service", "srv-test");

                //handle request

                span.SetResponseDetails(200, 43453423);
            }
        }

        public void HttpRequestCluster_Example()
        {
            using (var span = tracer.BeginHttpRequestClusterSpan())
            {
                span.SetRequestDetails(new Uri("current url"), "GET", 120323);

                //handle request

                span.SetClusterDetails("recursively", "success");
                span.SetResponseDetails(200, 43453423);
            }
        }
    }
}