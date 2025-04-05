namespace Company.Api.Contracts.Responses;

public class UserLoginResponse
{
    public string Name { get; init; } = default!;
    public string Token { get; init; } = default!;
}