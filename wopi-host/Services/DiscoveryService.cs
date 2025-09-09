using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace WopiHost.Services
{
    public class DiscoveryService
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _cfg;
        public DiscoveryService(IHttpClientFactory http, IConfiguration cfg)
        {
            _http = http; _cfg = cfg;
        }
        public async Task<string> GetDiscoveryXmlAsync()
        {
            var baseUrl = (_cfg["Collabora:BaseUrl"] ?? "").TrimEnd('/');
            var url = $"{baseUrl}/hosting/discovery";
            var client = _http.CreateClient();
            return await client.GetStringAsync(url);
        }
    }
}
