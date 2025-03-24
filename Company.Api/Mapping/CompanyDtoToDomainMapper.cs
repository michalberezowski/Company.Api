using Company.Api.Contracts.Data;
using Company.Api.Domain.Common;

namespace Company.Api.Mapping;

public static class CompanyDtoToDomainMapper
{
    public static Domain.Company ToCompany(this CompanyDto customerDto)
    {
        return new Domain.Company
        {
            Id = CompanyId.From(Guid.Parse(customerDto.Id)),
            Isin = ISIN.From(customerDto.Isin),
            Name = Name.From(customerDto.Name),
            StockTicker = StockTicker.From(customerDto.StockTicker),
            Exchange = Exchange.From(customerDto.Exchange),
            Website = customerDto.Website != null 
                ? Website.From(customerDto.Website) 
                : null
        };
    }
}
