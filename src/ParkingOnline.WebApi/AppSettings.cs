namespace ParkingOnline.WebApi;

public class AppSettings
{
	public string? AllowedHosts { get; set; }

	public Logging? Logging { get; set; }

	public ConnectionStrings? ConnectionStrings { get; set; }
}

public class Logging
{
	public LogLevel? LogLevel { get; set; }
}

public class LogLevel
{
	public string? Default { get; set; }

	public string? MicrosoftAspNetCore { get; set; }
}

public class ConnectionStrings
{
	public string? ParkingOnlineDBConnStr { get; set; }
}