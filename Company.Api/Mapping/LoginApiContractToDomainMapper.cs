using Company.Api.Contracts.Requests;
using Company.Api.Domain.Common;

namespace Company.Api.Mapping;

public static class LoginToDomainMapper
{
    public static Domain.Login ToLogin(this UserLoginRequest loginRequest)
    {
        return new Domain.Login
        {
            Name = UserName.From(loginRequest.Name),
            Password = Password.From(loginRequest.Password)
        };
    }
}
