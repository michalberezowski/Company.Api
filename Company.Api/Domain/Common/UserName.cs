using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Company.Api.Domain.Common;

public class UserName : ValueOf<string, UserName>
{
    private static readonly Regex FormatRegex =
        new(@"^[a-z0-9 ,.'\-]{4,30}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (FormatRegex.IsMatch(Value)) return;

        var message = $"{Value} is not a valid UserName. Only letters, numbers, space and the following symbols are allowed ,.'-" +
                      $"Allowed length is 4 to 30 characters";
        throw new ValidationException(message, [new ValidationFailure(nameof(UserName), message)]);
    }
}
