using ParkingOnline.WebApi;
using ParkingOnline.WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(appSettings =>
{
	builder.Configuration.Bind(appSettings);
});
builder.Services.RegistrarServicos();

var app = builder.Build();

app.ConfigurarScalar();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
