using Company.Api.Contracts.Responses;

namespace Company.Api.Mapping;

public static class CompanyDomainToApiContractMapper
{
    public static CompanyResponse ToCompanyResponse(this Domain.Company company)
    {
        return new CompanyResponse
        {
            Id = company.Id.Value,
            Isin = company.Isin.Value,
            Name = company.Name.Value,
            StockTicker = company.StockTicker.Value,
            Exchange = company.Exchange.Value,
            Website = company.Website?.Value  
        };
    }

    public static GetAllCompaniesResponse ToCompaniesResponse(this IEnumerable<Domain.Company> customers)
    {
        return new GetAllCompaniesResponse
        {
            Companies = customers.Select(x => new CompanyResponse
            {
                Id = x.Id.Value,
                Isin = x.Isin.Value,
                Name = x.Name.Value,
                StockTicker = x.StockTicker.Value,
                Exchange = x.Exchange.Value,
                Website = x.Website?.Value
            })
        };
    }
}
