using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Company.Api.Domain.Common;

public class Password : ValueOf<string, Password>
{

    private static readonly Regex FormatRegex =
        new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_\=\+\{\};:,<\.>]).{10,30}$", RegexOptions.Compiled);

    protected override void Validate()
    {
        //we are not creating users in this version, so password validation isn't necessary 

        //if (FormatRegex.IsMatch(Value)) return;

        //var message = $"{Value} is not a valid password. It must be:\n" +
        //              "  - 10 to 30 characters long, and contain at least one of each:\n" +
        //              "  - lowercase letter;\n" +
        //              "  - uppercase letter;\n" +
        //              "  - number;\n" + 
        //              "  - special character.";
        //throw new ValidationException(message, [new ValidationFailure(nameof(Password), message)]);
    }
}