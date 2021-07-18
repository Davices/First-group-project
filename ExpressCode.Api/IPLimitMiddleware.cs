using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpressCode.Api
{
    public class IPLimitMiddleware: IpRateLimitMiddleware
    {
        private readonly IpRateLimitOptions _options;
        private readonly IIpPolicyStore _ipPolicyStore;

        public IPLimitMiddleware(RequestDelegate next, IOptions<IpRateLimitOptions> options, IRateLimitCounterStore counterStore, IIpPolicyStore policyStore,
            IRateLimitConfiguration config, ILogger<IpRateLimitMiddleware> logger)
            : base(next, options, counterStore, policyStore, config, logger)
        {
            _options = options.Value;
            _ipPolicyStore = policyStore;
        }

        public override Task ReturnQuotaExceededResponse(HttpContext httpContext, RateLimitRule rule, string retryAfter)
        {
            var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = httpContext.Connection.RemoteIpAddress.ToString();
            }
            httpContext.Response.ContentType = "application/json";
            return httpContext.Response.WriteAsync($"{{ \"Code\": 429,\"msg\": \"操作频率过快，要求是: 每{rule.Period}秒{rule.Limit}次,请在{retryAfter}秒后再试!\" }}");
        }
    }
}
