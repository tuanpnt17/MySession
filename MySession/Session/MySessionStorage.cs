namespace MySession.Session
{
    /// <summary>
    /// This is where the app get and store session of all user
    /// </summary>
    public class MySessionStorage(IMySessionStorageEngine engine) : IMySessionStorage
    {
        private readonly Dictionary<string, ISession> _sessions = new();

        public ISession Create()
        {
            var newSession = new MySession(Guid.NewGuid().ToString("N"), engine);
            _sessions[newSession.Id] = newSession;
            return newSession;
        }

        public ISession Get(string id)
        {
            return _sessions.TryGetValue(id, out var session) ? session : Create();
        }
    }
}
