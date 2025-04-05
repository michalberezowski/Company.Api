using Company.Api.Contracts.Responses;
using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class GetCompanySummary : Summary<GetCompanyEndpoint>
{
    public GetCompanySummary()
    {
        Summary = "Returns a single Company by id";
        Description = "Returns a single Company by id";
        Response<CompanyResponse>(200, "Successfully found and returned the Company");
        Response(404, "The Company does not exist in the system");
    }
}
