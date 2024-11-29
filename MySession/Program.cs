var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var serviceCollection = builder.Services;

serviceCollection.AddControllersWithViews();

serviceCollection.AddSingleton<IMySessionStorageEngine>(services =>
{
    var path = Path.Combine(
        services.GetRequiredService<IHostEnvironment>().ContentRootPath,
        "sessions"
    );
    Directory.CreateDirectory(path);

    return new MySessionStorageEngine(path);
});
serviceCollection.AddSingleton<IMySessionStorage, MySessionStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
