using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Company.Api.Domain.Common;

public class Website : ValueOf<string, Website>
{
    private static readonly Regex FormatRegex =
        new(@"^(https?://)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*/?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (Value.Equals(string.Empty) || FormatRegex.IsMatch(Value)) return;

        var message = $"{Value} is not a valid website URL.";
        throw new ValidationException(message, [new ValidationFailure(nameof(Website), message)]);
    }
}