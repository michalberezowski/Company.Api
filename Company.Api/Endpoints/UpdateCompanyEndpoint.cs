using Company.Api.Contracts.Requests;
using Company.Api.Contracts.Responses;
using Company.Api.Mapping;
using Company.Api.Services;
using FastEndpoints;

namespace Company.Api.Endpoints;

[HttpPut("companies/{id:guid}")]
public class UpdateCompanyEndpoint(ICompanyService companyService) : Endpoint<UpdateCompanyRequest, CompanyResponse>
{
    public override async Task HandleAsync(UpdateCompanyRequest req, CancellationToken ct)
    {
        var existingCompany = await companyService.GetAsync(req.Id);

        if (existingCompany is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var company = req.ToCompany();
        await companyService.UpdateAsync(company);

        var customerResponse = company.ToCompanyResponse();
        await SendOkAsync(customerResponse, ct);
    }
}
