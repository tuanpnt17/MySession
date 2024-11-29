namespace MySession.Session
{
    public interface IMySessionStorageEngine
    {
        Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken);
        Task CommitAsync(
            string id,
            Dictionary<string, byte[]> store,
            CancellationToken cancellationToken
        );
    }
}
