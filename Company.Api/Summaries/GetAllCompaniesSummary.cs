using Company.Api.Contracts.Responses;
using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class GetAllCompaniesSummary : Summary<GetAllCompaniesEndpoint>
{
    public GetAllCompaniesSummary()
    {
        Summary = "Returns all the Companies in the system";
        Description = "Returns all the Companies in the system";
        Response<GetAllCompaniesResponse>(200, "All Companies in the system are returned");
    }
}
