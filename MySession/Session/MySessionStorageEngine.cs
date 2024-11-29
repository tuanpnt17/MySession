using System.Text.Json;

namespace MySession.Session
{
    public class MySessionStorageEngine(string dictionaryPath) : IMySessionStorageEngine
    {
        public async Task<Dictionary<string, byte[]>> LoadAsync(
            string id,
            CancellationToken cancellationToken
        )
        {
            var filePath = Path.Combine(dictionaryPath, id);
            if (!File.Exists(filePath))
                return [];

            await using var fileStream = new FileStream(filePath, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);
            var json = await streamReader.ReadToEndAsync(cancellationToken);
            return JsonSerializer.Deserialize<Dictionary<string, byte[]>>(json) ?? [];
        }

        public async Task CommitAsync(
            string id,
            Dictionary<string, byte[]> store,
            CancellationToken cancellationToken
        )
        {
            var filePath = Path.Combine(dictionaryPath, id);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await using var streamWriter = new StreamWriter(fileStream);

            await streamWriter.WriteAsync(JsonSerializer.Serialize(store));
        }
    }
}
