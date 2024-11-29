namespace MySession.Session
{
    public interface IMySessionStorage
    {
        ISession Create();
        ISession Get(string id);
    }
}
