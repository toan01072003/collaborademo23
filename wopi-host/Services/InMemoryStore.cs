using System.Collections.Concurrent;

namespace WopiHost.Services
{
    public class InMemoryStore
    {
        private readonly ConcurrentDictionary<string, FileEntry> _map = new();
        public bool TryGet(string id, out FileEntry entry) => _map.TryGetValue(id, out entry!);
        public void Set(string id, FileEntry entry) => _map[id] = entry;
        public bool Exists(string id) => _map.ContainsKey(id);
    }

    public class FileEntry
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string OwnerId { get; set; } = "user-123";
        public long Version { get; set; }
        public byte[] Bytes { get; set; } = System.Array.Empty<byte>();
    }
}
