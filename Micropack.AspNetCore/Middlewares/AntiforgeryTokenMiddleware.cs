﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;

namespace Micropack.AspNetCore.Middlewares
{
    public class AntiforgeryTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antiforgery;

        public AntiforgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;
            if (path != null && !path.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
            {
                var tokens = _antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append(
                      key: "XSRF-TOKEN",
                      value: tokens.RequestToken,
                      options: new CookieOptions
                      {
                          HttpOnly = false // Now JavaScript is able to read the cookie
                      });
            }
            return _next(context);
        }
    }
}
