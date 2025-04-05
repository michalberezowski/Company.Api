﻿namespace Company.Api.Contracts.Data;

public class CompanyDto
{
    public string Id { get; init; } = default!;
    public string? Isin { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string StockTicker { get; init; } = default!;
    public string Exchange { get; init; } = default!;
    public string? Website { get; init; }
}
