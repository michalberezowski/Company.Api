using Company.Api.Contracts.Requests;
using Company.Api.Contracts.Responses;
using Company.Api.Mapping;
using Company.Api.Services;
using FastEndpoints;

namespace Company.Api.Endpoints;

[HttpGet("companies/isin/{isin}")]
public class GetCompanyByIsinEndpoint(ICompanyService companyService) : Endpoint<GetCompanyByIsinRequest, CompanyResponse>
{
    public override async Task HandleAsync(GetCompanyByIsinRequest req, CancellationToken ct)
    {
        var company = await companyService.GetByIsinAsync(req.Isin);

        if (company is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var companyResponse = company.ToCompanyResponse();
        await SendOkAsync(companyResponse, ct);
    }
}
