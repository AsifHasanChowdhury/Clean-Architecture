// Infrastructure/Db/SqlConnectionFactory.cs
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Db;

public interface IConnectionFactory
{
    IDbConnection Create();
}

public sealed class SqlConnectionFactory(IConfiguration config) : IConnectionFactory
{
    private readonly string _cs = config.GetConnectionString("Default")
        ?? throw new InvalidOperationException("Missing ConnectionStrings:Default");
    public IDbConnection Create() => new SqlConnection(_cs);
}
