using Company.Api.Contracts.Responses;
using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class CreateCompanySummary : Summary<CreateCompanyEndpoint>
{
    public CreateCompanySummary()
    {
        Summary = "Creates a new company in the system";
        Description = "Creates a new company in the system";
        Response<CompanyResponse>(201, "Company was successfully created");
        Response<FailureResponse>(400, "The request did not pass validation checks");
    }
}
