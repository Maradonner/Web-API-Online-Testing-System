using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TestingSystem.HttpHandler
{
    public class HttpTrackerHandler: DelegatingHandler
    {
        private readonly IHttpContextAccessor _context;

        public HttpTrackerHandler(IHttpContextAccessor context)
        {
            _context = context;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_context.HttpContext.Request.Headers.TryGetValue("Authorization", out var jwt))
                request.Headers.Add("Authorization", jwt.FirstOrDefault());

            if (_context.HttpContext.Request.Headers.TryGetValue("TraceId", out var traceid))
                request.Headers.Add("TraceId", traceid.FirstOrDefault());

            return base.SendAsync(request, cancellationToken);
        }
    }
}
