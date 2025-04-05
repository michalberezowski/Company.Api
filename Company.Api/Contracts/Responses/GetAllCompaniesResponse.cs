namespace Company.Api.Contracts.Responses;

public class GetAllCompaniesResponse
{
    public IEnumerable<CompanyResponse> Companies { get; init; } = [];
}
