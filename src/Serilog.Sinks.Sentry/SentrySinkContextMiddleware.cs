﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Serilog.Sinks.Sentry
{
    internal class SentrySinkContextMiddleware
    {
        private readonly RequestDelegate _next;

        public SentrySinkContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty(Constants.HttpContextKey, context, true))
            {
                await _next(context)
                    .ConfigureAwait(false);
            }
        }
    }
}