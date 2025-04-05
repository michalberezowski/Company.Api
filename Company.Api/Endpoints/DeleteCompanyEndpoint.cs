using Company.Api.Contracts.Requests;
using Company.Api.Services;
using FastEndpoints;

namespace Company.Api.Endpoints;

[HttpDelete("companies/{id:guid}")]
public class DeleteCompanyEndpoint(ICompanyService companyService) : Endpoint<DeleteCompanyRequest>
{
    public override async Task HandleAsync(DeleteCompanyRequest req, CancellationToken ct)
    {
        var deleted = await companyService.DeleteAsync(req.Id);
        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}
