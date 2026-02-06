using Scrutor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7049/api/") });

builder.Services.Scan(selector => selector.FromAssemblies(typeof(Program).Assembly)
                                          .AddClasses(filter => filter.Where(type => type.Name.EndsWith("Service")), publicOnly: false)
                                          .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                                          .AsMatchingInterface()
                                          .WithScopedLifetime());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
