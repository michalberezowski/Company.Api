using Company.Api.Contracts.Data;

namespace Company.Api.Mapping;

public static class CompanyDomainToDtoMapper
{
    public static CompanyDto ToCompanyDto(this Domain.Company company)
    {
        return new CompanyDto
        {
            Id = company.Id.Value.ToString(),
            Isin = company.Isin.Value,
            Name = company.Name.Value,
            StockTicker = company.StockTicker.Value,
            Exchange = company.Exchange.Value,
            Website = company.Website?.Value
        };
    }
}
