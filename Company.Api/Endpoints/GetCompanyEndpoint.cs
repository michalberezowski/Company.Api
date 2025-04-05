using Company.Api.Contracts.Requests;
using Company.Api.Contracts.Responses;
using Company.Api.Mapping;
using Company.Api.Services;
using FastEndpoints;

namespace Company.Api.Endpoints;

[HttpGet("companies/{id:guid}")]
public class GetCompanyEndpoint(ICompanyService customerService) : Endpoint<GetCompanyRequest, CompanyResponse>
{
    public override async Task HandleAsync(GetCompanyRequest req, CancellationToken ct)
    {
        var company = await customerService.GetAsync(req.Id);

        if (company is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var companyResponse = company.ToCompanyResponse();
        await SendOkAsync(companyResponse, ct);
    }
}
