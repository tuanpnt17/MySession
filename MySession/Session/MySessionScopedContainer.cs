namespace MySession.Session
{
    public class MySessionScopedContainer
    {
        public MySessionScopedContainer(ILogger<MySessionScopedContainer> logger)
        {
            logger.LogInformation("Initial MySessionScopedContainer");
        }

        public ISession? Session { get; set; }
    }
}
