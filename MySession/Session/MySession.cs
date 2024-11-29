using System.Diagnostics.CodeAnalysis;

namespace MySession.Session
{
    public class MySession(string id, IMySessionStorageEngine engine) : ISession
    {
        private readonly Dictionary<string, byte[]> _store = new();

        public bool IsAvailable
        {
            get
            {
                LoadAsync(CancellationToken.None).Wait();
                return true;
            }
        }

        public string Id => id;
        public IEnumerable<string> Keys => _store.Keys;

        public async Task LoadAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _store.Clear();
            var loadedStore = await engine.LoadAsync(id, cancellationToken);
            foreach (var pair in loadedStore)
            {
                _store[pair.Key] = pair.Value;
            }
        }

        public async Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await engine.CommitAsync(id, _store, cancellationToken);
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
        {
            return _store.TryGetValue(key, out value);
        }

        public void Set(string key, byte[] value)
        {
            _store[key] = value;
        }

        public void Remove(string key)
        {
            _store.Remove(key);
        }

        public void Clear()
        {
            _store.Clear();
        }
    }
}
