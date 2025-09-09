using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web;
using WopiHost.Services;

namespace WopiHost.Controllers
{
    public class UiController : Controller
    {
        private readonly IConfiguration _cfg;
        private readonly DiscoveryService _discovery;
        private readonly InMemoryStore _store;

        public UiController(IConfiguration cfg, DiscoveryService discovery, InMemoryStore store)
        {
            _cfg = cfg;
            _discovery = discovery;
            _store = store;
        }

        public IActionResult Index()
        {
            // Ensure demo file exists
            if (!_store.Exists("1"))
            {
                _store.Set("1", new FileEntry
                {
                    Id = "1",
                    Name = "demo-1.odt",
                    OwnerId = "user-123",
                    Bytes = System.Text.Encoding.UTF8.GetBytes("Hello Collabora on Railway!\n"),
                    Version = System.DateTimeOffset.UtcNow.ToUnixTimeSeconds()
                });
            }

            ViewBag.AppBase = _cfg["App:BaseUrl"] ?? "";
            ViewBag.Collabora = _cfg["Collabora:BaseUrl"] ?? "";
            ViewBag.FileId = "1"; // simple demo
            return View();
        }

        [HttpPost]
        public IActionResult Open(string fileId)
        {
            var appBase = (_cfg["App:BaseUrl"] ?? "").TrimEnd('/');
            var collaboraBase = (_cfg["Collabora:BaseUrl"] ?? "").TrimEnd('/');
            var wopiSrc = HttpUtility.UrlEncode($"{appBase}/wopi/files/{fileId}");
            var editorUrl = $"{collaboraBase}/browser/0b39b/cool.html?WOPISrc={wopiSrc}";

            ViewBag.EditorUrl = editorUrl;
            ViewBag.AccessToken = "test"; // demo only
            return View("Editor");
        }
    }
}
