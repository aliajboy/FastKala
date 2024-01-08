using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FastKala.Application.Data;
public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SqlServer") ?? throw new InvalidOperationException("اتصال با پایگاه داده برقرار نشد");
    }
    public SqlConnection CreateConnection()
        => new SqlConnection(_connectionString);
}