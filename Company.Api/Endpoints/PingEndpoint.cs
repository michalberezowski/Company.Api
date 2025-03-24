using Company.Api.Contracts.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace Company.Api.Endpoints;

[HttpGet("ping"), AllowAnonymous]
public class PingEndpoint : EndpointWithoutRequest
{
    public override async Task HandleAsync(CancellationToken ct) => 
        await SendOkAsync(new PingResponse(), ct);
}
