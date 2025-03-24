namespace Company.Api.Domain;

public class Login

{
    public Common.UserName Name { get; init; } = null!;
    public Common.Password Password { get; init; } = null!;

}