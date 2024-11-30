namespace MySession.Extensions
{
    public static class MySessionExtensions
    {
        public static IServiceCollection AddMySession(this IServiceCollection services)
        {
            services.AddSingleton<IMySessionStorageEngine>(serviceProvider =>
            {
                var path = Path.Combine(
                    serviceProvider.GetRequiredService<IHostEnvironment>().ContentRootPath,
                    "sessions"
                );
                Directory.CreateDirectory(path);

                return new MySessionStorageEngine(path);
            });
            services.AddSingleton<IMySessionStorage, MySessionStorage>();
            services.AddScoped<MySessionScopedContainer>();

            return services;
        }
    }
}
