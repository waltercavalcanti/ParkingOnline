using Microsoft.Data.SqlClient;

namespace ParkingOnline.WebApi.Shared.Data;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("ParkingOnlineDBConnStr")!;

    public SqlConnection CreateConnection() => new(_connectionString);
}