using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class DeleteCompanySummary : Summary<DeleteCompanyEndpoint>
{
    public DeleteCompanySummary()
    {
        Summary = "Deletes a Company the system";
        Description = "Deletes a Company the system";
        Response(204, "The Company was deleted successfully");
        Response(404, "The Company was not found in the system");
    }
}
