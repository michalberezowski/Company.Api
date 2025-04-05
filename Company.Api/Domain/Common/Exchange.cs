using FluentValidation.Results;
using System.Text.RegularExpressions;
using FluentValidation;
using ValueOf;

namespace Company.Api.Domain.Common;

public class Exchange : ValueOf<string, Exchange>
{
    private static readonly Regex FormatRegex =
        new(@"^[\p{L}0-9., ]{1,50}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (FormatRegex.IsMatch(Value)) return;

        var message = $"{Value} is not a valid exchange name. " +
                      $"Only letters, numbers, space, comma and a period are allowed. " +
                      $"Allowed length is 1 to 50 characters in total.";
        throw new ValidationException(message, [new ValidationFailure(nameof(Exchange), message)]);
    }
}