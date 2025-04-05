using Company.Api.Contracts.Requests;
using Company.Api.Domain.Common;

namespace Company.Api.Mapping;

public static class CompanyToDomainMapper
{
    public static Domain.Company ToCompany(this CreateCompanyRequest request)
    {
        return new Domain.Company
        {
            Id = CompanyId.From(Guid.NewGuid()),
            Isin = ISIN.From(request.Isin),
            Name = Name.From(request.Name),
            StockTicker = StockTicker.From(request.StockTicker),
            Exchange = Exchange.From(request.Exchange),
            Website = request.Website != null ? Website.From(request.Website) : null
        };
    }    
    
    public static Domain.Company ToCompany(this UpdateCompanyRequest request)
    {
        return new Domain.Company
        {
            Id = CompanyId.From(request.Id),
            Isin = ISIN.From(request.Isin),
            Name = Name.From(request.Name),
            StockTicker = StockTicker.From(request.StockTicker),
            Exchange = Exchange.From(request.Exchange),
            Website = request.Website != null ? Website.From(request.Website) : null
        };
    }

}
