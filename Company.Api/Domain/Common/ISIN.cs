using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Company.Api.Domain.Common;

// ReSharper disable once InconsistentNaming - ISIN is an acronym
public class ISIN : ValueOf<string, ISIN>
{
    private static readonly Regex FormatRegex = 
        new("^[A-Za-z]{2}[A-Za-z0-9]{10}$", RegexOptions.Compiled);

    protected override void Validate()
    {
        if (FormatRegex.IsMatch(Value)) return;

        var message = $"{Value} is not a valid ISIN. It should consist of two letters followed by ten digits/letters.";
        throw new ValidationException(message, [new ValidationFailure(nameof(ISIN), message)]);
    }
}
