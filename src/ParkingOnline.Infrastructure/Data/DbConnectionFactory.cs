using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.Infrastructure.Data;

public class DbConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("ParkingOnlineDBConnStr")!;

    public SqlConnection CreateConnection() => new(_connectionString);
}