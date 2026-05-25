using Carter;
using ParkingOnline.WebApi;
using ParkingOnline.WebApi.Startup;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(appSettings =>
{
    builder.Configuration.Bind(appSettings);
});
builder.Services.RegistrarServicos();

WebApplication app = builder.Build();

app.ConfigurarScalar();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapCarter();

app.Run();
