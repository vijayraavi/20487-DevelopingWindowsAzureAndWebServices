using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Mod03
{
    public class CustomMiddleware
    {
        RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            await _next(context);

            if (context.Response.ContentType.ToLower().Contains("text/html"))
            {
                long elapsed = sw.ElapsedMilliseconds;
                context.Response.Headers.Add("X-ElapsedTime-Demo", new[] { elapsed.ToString() });
                sw.Stop();

                Debug.WriteLine("Processing time: " + elapsed.ToString() + "ms");
            }
        }
    }
}
