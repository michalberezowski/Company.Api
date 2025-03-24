namespace Company.Api.Contracts.Requests;

public class GetCompanyByIsinRequest
{
    //[QueryParam] 
    public string Isin { get; init; } = default!;
}