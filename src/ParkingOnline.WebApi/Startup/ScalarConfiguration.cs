using Scalar.AspNetCore;

namespace ParkingOnline.WebApi.Startup;

public static class ScalarConfiguration
{
	public static WebApplication ConfigurarScalar(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.MapScalarApiReference();
			app.MapOpenApi();
		}

		return app;
	}
}