﻿using System.Threading.Tasks;
using Owin;
using Prometheus;
using Prometheus.Advanced;

namespace Toolfactory.Prometheus.Extensions
{
    /// <summary>
    /// Extensions for adding Prometheus endpoint into an OWIN pipeline
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Enables an endpoint which exposes metrics that can be consumed by Prometheus server
        /// </summary>
        /// <param name="app"></param>
        /// <param name="endpoint">The endpoint where the metrics are exposed. /metrics by default</param>
        /// <param name="collectorRegistry"></param>
        public static void UsePrometheus(this IAppBuilder app, string endpoint = "/metrics", ICollectorRegistry collectorRegistry = null)
        {

            var registry = collectorRegistry ?? DefaultCollectorRegistry.Instance;

            if (registry == DefaultCollectorRegistry.Instance)
            {
                DefaultCollectorRegistry.Instance.RegisterOnDemandCollectors(new[] { new DotNetStatsCollector() });
            }

            app.Map(endpoint, builder =>
            {
                builder.Run(context =>
                {
                    var acceptHeader = context.Request.Headers.Get("Accept");
                    var acceptHeaders = acceptHeader?.Split(',');
                    var contentType = ScrapeHandler.GetContentType(acceptHeaders);

                    context.Response.ContentType = contentType;
                    
                    using (var stream = context.Response.Body)
                    {
                        var collected = registry.CollectAll();
                        ScrapeHandler.ProcessScrapeRequest(collected, contentType, stream);

                    }

                    return Task.FromResult(0);
                });
            });
        }
    }
}
