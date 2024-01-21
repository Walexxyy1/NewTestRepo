using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        IValidate _v;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IValidate v)
        {
            _v = v;
            string authorizationHeader = httpContext.Request.Headers["Authorization"];
            if (authorizationHeader != null && authorizationHeader.StartsWith("Basic"))
            {
                string encodedCredentials = authorizationHeader.Substring("Basic".Length).Trim();
                Encoding encode = Encoding.GetEncoding("UTF-8");
                string userpass = encode.GetString(Convert.FromBase64String(encodedCredentials));
                int index = userpass.IndexOf(":");
                var username = userpass.Substring(0, index);
                var password = userpass.Substring(index + 1);

                if (_v.getAuthorisedUser(username, password))
                {
                    await _next.Invoke(httpContext);
                }
            }
            else
            {
                httpContext.Response.StatusCode = 401;
                return;
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
