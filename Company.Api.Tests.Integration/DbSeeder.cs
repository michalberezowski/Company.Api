using Company.Api.Contracts.Data;
using Company.Api.Database;
using Company.Api.Domain.Common;
using Company.Api.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Api.Tests.Integration;

/// <summary>
/// initialize the database with the data for the integration tests. Provide data for the tests cases
/// </summary>
internal class DbSeeder
{
    internal string CompanyIdForDelete = Guid.NewGuid().ToString();
    internal string CompanyIdForUpdate = Guid.NewGuid().ToString();

    public record BaseCompany(
        string Name = "Company Ltd.",
        string Exchange = "NYSE",
        string Isin = "US0000000000",
        string StockTicker = "COMP",
        string Website = "https://www.aabbcc.local");
    
    public static BaseCompany CompanyForCreate => new() { Isin = "US1111111111" };
    public static ISIN DuplicateIsinForCreate => ISIN.From("US1234567891"); // not seeded, created in the test
    public static BaseCompany CompanyForUpdate => new() { Isin = "US2222222222" };
    public static BaseCompany CompanyForDelete => new() { Isin = "US3333333333" };
    public static BaseCompany CompanyForUpdateDup => new() { Isin = "US4444444444" };
    
    internal async Task InitAsync(IServiceProvider scopedServices)
    {
        var databaseInitializer = scopedServices.GetRequiredService<DatabaseInitializer>();
        await databaseInitializer.ResetAsync();
        await databaseInitializer.InitializeAsync();

        var companies = new List<CompanyDto>
        {
            new() { Id = CompanyIdForDelete,        Name = "Company For Deletion Ltd.",   Exchange = "NYSE",                 Isin = CompanyForDelete.Isin,    StockTicker = "CDEL", Website = "https://www.cdel.local" },
            new() { Id = CompanyIdForUpdate,        Name = "Company For Update Ltd.",     Exchange = "NYSE",                 Isin = CompanyForUpdate.Isin,    StockTicker = "CUPD", Website = "https://www.cupd.local" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Company For Update Dup Ltd.", Exchange = "NYSE",                 Isin = CompanyForUpdateDup.Isin, StockTicker = "CUDU", Website = "https://www.cudu.local" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Apple Inc.",                  Exchange = "NASDAQ",               Isin = "US0378331005",           StockTicker = "AAPL", Website = "http://www.apple.com" },
            new() { Id = Guid.NewGuid().ToString(), Name = "British Airways Plc",         Exchange = "Pink Sheets",          Isin = "US1104193065",           StockTicker = "BAIRY", Website = null },
            new() { Id = Guid.NewGuid().ToString(), Name = "Heineken NV",                 Exchange = "Euronext Amsterdam",   Isin = "NL0000009165",           StockTicker = "HEIA", Website = null },
            new() { Id = Guid.NewGuid().ToString(), Name = "Panasonic Corp",              Exchange = "Tokyo Stock Exchange", Isin = "JP3866800000",           StockTicker = "6752", Website = "http://www.panasonic.co.jp" },
            new() { Id = Guid.NewGuid().ToString(), Name = "Porsche Automobil",           Exchange = "Deutsche Börse",       Isin = "DE000PAH0038",           StockTicker = "PAH3", Website = "https://www.porsche.com/" }
        };

        var repo = scopedServices.GetRequiredService<ICompanyRepository>();
        foreach (var company in companies)
        {
            await repo.CreateAsync(company);
        }
    }
}