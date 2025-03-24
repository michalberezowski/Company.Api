using Company.Api.Contracts.Requests;
using Company.Api.Contracts.Responses;
using Company.Api.Mapping;
using Company.Api.Services;
using FastEndpoints;

namespace Company.Api.Endpoints;

[HttpPost("companies")]
public class CreateCompanyEndpoint(ICompanyService companyService) : Endpoint<CreateCompanyRequest, CompanyResponse>
{
    public override async Task HandleAsync(CreateCompanyRequest req, CancellationToken ct)
    {
        var company = req.ToCompany();

        await companyService.CreateAsync(company);

        var companyResponse = company.ToCompanyResponse();
        await SendCreatedAtAsync<GetCompanyEndpoint>(
            new { Id = company.Id.Value }, companyResponse, generateAbsoluteUrl: true, cancellation: ct);
    }
}
