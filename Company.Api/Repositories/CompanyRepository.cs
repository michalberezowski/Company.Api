using Company.Api.Contracts.Data;
using Company.Api.Database;
using Company.Api.Domain.Common.Exceptions;
using Dapper;
using FluentValidation.Results;
using Microsoft.Data.Sqlite; 

namespace Company.Api.Repositories; 
public interface ICompanyRepository
{
    Task<bool> CreateAsync(CompanyDto customer);
    Task<CompanyDto?> GetAsync(string id);
    Task<IEnumerable<CompanyDto>> GetAllAsync();
    Task<bool> UpdateAsync(CompanyDto customer);
    Task<bool> DeleteAsync(string id);
    Task<CompanyDto?> GetByIsinAsync(string? isin);
}

public class CompanyRepository(IDbConnectionFactory connectionFactory) : ICompanyRepository
{
    public async Task<bool> CreateAsync(CompanyDto company)
    {
        try
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"INSERT INTO Companies (Id, Isin, Name, StockTicker, Exchange, Website) 
                VALUES (@Id, @Isin, @Name, @StockTicker, @Exchange, @Website)",
                company);
            return result > 0;
        }
        catch (SqliteException e) when (e.SqliteErrorCode == 19) // SQLite constraint violation error code
        {
            var message = $"Cannot create a company with ISIN {company.Isin}. Company with that ISIN already exists";
            throw new UniqueViolationException([new ValidationFailure(nameof(Domain.Company.Isin), message)]);
        }
    }

    public async Task<CompanyDto?> GetAsync(string id)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<CompanyDto>(
            "SELECT * FROM Companies WHERE Id = @Id LIMIT 1", new { Id = id });
    }

    public async Task<CompanyDto?> GetByIsinAsync(string? isin)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<CompanyDto>(
            "SELECT * FROM Companies WHERE Isin = @Isin LIMIT 1", new { Isin = isin });
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        return await connection.QueryAsync<CompanyDto>("SELECT * FROM Companies");
    }

    public async Task<bool> UpdateAsync(CompanyDto company)
    {
        try
        {
            using var connection = await connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"UPDATE Companies 
                SET Isin = @Isin, Name = @Name, StockTicker = @StockTicker, Exchange = @Exchange, Website = @Website 
                WHERE Id = @Id",
                company);
            return result > 0;
        }
        catch (SqliteException e) when (e.SqliteErrorCode == 19) // SQLite constraint violation error code
        {
            var message = $"Cannot change Isin to {company.Isin}. Company with that ISIN already exists";
            throw new UniqueViolationException([new ValidationFailure(nameof(Domain.Company.Isin), message)]);
        }
    }

    public async Task<bool> DeleteAsync(string id)
    {
        using var connection = await connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"DELETE FROM Companies WHERE Id = @Id",
            new {Id = id});
        return result > 0;
    }
}
