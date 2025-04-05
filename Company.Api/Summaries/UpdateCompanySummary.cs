using Company.Api.Contracts.Responses;
using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class UpdateCompanySummary : Summary<UpdateCompanyEndpoint>
{
    public UpdateCompanySummary()
    {
        Summary = "Updates an existing Company in the system";
        Description = "Updates an existing Company in the system";
        Response<CompanyResponse>(201, "Company was successfully updated");
        Response<FailureResponse>(400, "The request did not pass validation checks");
    }
}
