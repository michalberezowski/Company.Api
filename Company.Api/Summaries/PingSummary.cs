using Company.Api.Contracts.Responses;
using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class PingSummary : Summary<PingEndpoint>
{
    public PingSummary()
    {
        Summary = "Health check";
        Description = "Health check";
        Response<PingResponse>(200, "Api is running");
    }
}
