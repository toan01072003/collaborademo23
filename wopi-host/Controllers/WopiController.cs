using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WopiHost.Services;

namespace WopiHost.Controllers
{
    [ApiController]
    [Route("wopi/files")]
    public class WopiController : ControllerBase
    {
        private readonly InMemoryStore _store;
        public WopiController(InMemoryStore store)
        {
            _store = store;
        }

        // GET /wopi/files/{id} -> CheckFileInfo
        [HttpGet("{id}")]
        public IActionResult CheckFileInfo(string id)
        {
            if (!_store.TryGet(id, out var file)) return NotFound();

            var info = new
            {
                BaseFileName = file.Name,
                Size = file.Bytes.Length,
                OwnerId = file.OwnerId,
                Version = file.Version.ToString(),
                UserId = file.OwnerId,
                UserFriendlyName = file.OwnerId,
                SupportsGetLock = true,
                SupportsLocks = true,
                SupportsUpdate = true,
                UserCanWrite = true,
                PostMessageOrigin = Request.Headers["Origin"].ToString()
            };
            return Ok(info);
        }

        // GET /wopi/files/{id}/contents -> file bytes
        [HttpGet("{id}/contents")]
        public IActionResult GetFile(string id)
        {
            if (!_store.TryGet(id, out var file)) return NotFound();
            return File(file.Bytes, "application/octet-stream");
        }

        // POST /wopi/files/{id}/contents -> save
        [HttpPost("{id}/contents")]
        public async Task<IActionResult> PutFile(string id)
        {
            if (!_store.TryGet(id, out var file)) return NotFound();
            using var ms = new MemoryStream();
            await Request.Body.CopyToAsync(ms);
            file.Bytes = ms.ToArray();
            file.Version = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            _store.Set(id, file);
            return Ok();
        }
    }
}
