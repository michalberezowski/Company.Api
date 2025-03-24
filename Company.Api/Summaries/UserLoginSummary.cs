using Company.Api.Contracts.Responses;
using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class UserLoginSummary : Summary<UserLoginEndpoint>
{
    public UserLoginSummary()
    {
        Summary = "Returns JWT token for accessing authorized endpoints";
        Description = "Returns JWT token for accessing authorized endpoints";
        Response<UserLoginResponse>(200, "JWT token has been issued");
        Response<FailureResponse>(400, "The request did not pass validation checks");
        Response<FailureResponse>(400, "Login failed");
    }
}
