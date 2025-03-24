using Company.Api.Domain;
using FastEndpoints.Security;

namespace Company.Api.Services;

public interface ICompanyAuthenticationService
{
    bool IsLoginSuccessful(Login login);
    string JwtToken(string userName, string jwtKey);
}

public class CompanyAuthenticationService : ICompanyAuthenticationService
{
    public bool IsLoginSuccessful(Domain.Login login)
    {
        //todo: this would have to be replaced with a real authentication mechanism;
        //store salted password hashes w/ nonce securely, etc.
        //hardcoded two users for the sake of the example
        return
            (login.Name.Value.Equals("admin", StringComparison.InvariantCultureIgnoreCase) &&
             login.Password.Value == "Qqqq11111!") 
            ||
            (login.Name.Value.Equals("user", StringComparison.InvariantCultureIgnoreCase) &&
             login.Password.Value == "Qqqq11111!");
    }

    public string JwtToken(string userName, string jwtKey)
    {
        var jwtToken = JwtBearer.CreateToken(
            opts =>
            {
                opts.SigningKey = jwtKey;
                opts.ExpireAt = DateTime.UtcNow.AddDays(1);
                opts.User.Claims.Add(("UserName", userName));
            });
        return jwtToken;
    }
}
