using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace CaixaModernApi.Security.IpAllowlist
{
    public class IpAllowlistAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly HashSet<string> _allowed;
        public IpAllowlistAuthorizationFilter(IOptions<IpAllowlistOptions> options) 
            => _allowed = GetAllowedIpList(options);

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (_allowed.Contains("*"))
                return Task.CompletedTask;

            var ipAddressText = context.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
            if (!_allowed.Contains(ipAddressText))
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);

            return Task.CompletedTask;
        }

        private HashSet<string> GetAllowedIpList(IOptions<IpAllowlistOptions> options)
        {
            var ips = options.Value.AllowedIPs?.Where(ip => !string.IsNullOrWhiteSpace(ip))?.Select(ip => ip.Trim());
            if (ips == null)
                return new HashSet<string>();

            return ips.ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
    }
}