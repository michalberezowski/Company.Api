using Company.Api.Contracts.Responses;
using Company.Api.Mapping;
using Company.Api.Services;
using FastEndpoints;

namespace Company.Api.Endpoints;

[HttpGet("companies")]
public class GetAllCompaniesEndpoint(ICompanyService companyService) : EndpointWithoutRequest<GetAllCompaniesResponse>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        var customers = await companyService.GetAllAsync();
        var customersResponse = customers.ToCompaniesResponse();
        await SendOkAsync(customersResponse, ct);
    }
}
