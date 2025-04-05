namespace Company.Api.Domain;

public class Company
{
    public Common.CompanyId Id { get; init; } = Common.CompanyId.From(Guid.NewGuid());
    public Common.ISIN Isin { get; init; } = null!;
    public Common.Name Name { get; init; } = null!;
    public Common.StockTicker StockTicker { get; init; } = null!;
    public Common.Exchange Exchange { get; init; } = null!;
    public Common.Website? Website { get; init; }
}