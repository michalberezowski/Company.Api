namespace Company.Api.Contracts.Requests;

public class UserLoginRequest
{
    public string Name { get; init; } = default!;
    public string Password { get; init; } = default!;
}