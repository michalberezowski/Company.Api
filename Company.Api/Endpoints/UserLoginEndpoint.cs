using Company.Api.Contracts.Requests;
using Company.Api.Contracts.Responses;
using Company.Api.Mapping;
using Company.Api.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace Company.Api.Endpoints;

[HttpPost("/login"), AllowAnonymous]
public class UserLoginEndpoint(ICompanyAuthenticationService authService, IConfiguration config) 
    : Endpoint<UserLoginRequest,UserLoginResponse>
{
    public override async Task HandleAsync(UserLoginRequest loginRequest, CancellationToken ct)
    {
        if (!authService.IsLoginSuccessful(loginRequest.ToLogin()))
        {
            ThrowError("The supplied credentials are invalid.");
        }

        var jwtToken = authService.JwtToken(loginRequest.Name, config.GetValue<string>("Auth:TokenSecret")!);
        var resp = new UserLoginResponse { Name = loginRequest.Name, Token = jwtToken };

        await SendAsync(resp, cancellation: ct);
    }
}