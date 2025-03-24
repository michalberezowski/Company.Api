using Dapper;

namespace Company.Api.Database;

public class DatabaseInitializer(IDbConnectionFactory connectionFactory)
{
    public async Task InitializeAsync()
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS Companies (
            Id CHAR(36) PRIMARY KEY, 
            Isin TEXT NOT NULL UNIQUE,
            Name TEXT NOT NULL,
            StockTicker TEXT NOT NULL,
            Exchange TEXT NOT NULL,
            Website TEXT NULL,
            CONSTRAINT unique_isin UNIQUE (Isin))"); 
    }

    public async Task ResetAsync()
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync("DROP TABLE IF EXISTS Companies");
    }
}
