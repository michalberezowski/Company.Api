namespace Company.Api.Contracts.Requests;

public class CreateCompanyRequest
{
    public string Isin { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string StockTicker { get; init; } = default!;
    public string Exchange { get; init; } = default!;
    public string? Website { get; init; }
}
