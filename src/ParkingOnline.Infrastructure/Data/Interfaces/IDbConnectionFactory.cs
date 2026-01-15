using Microsoft.Data.SqlClient;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IDbConnectionFactory
{
    SqlConnection CreateConnection();
}