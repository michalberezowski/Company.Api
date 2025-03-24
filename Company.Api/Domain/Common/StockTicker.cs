using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Company.Api.Domain.Common;

public class StockTicker : ValueOf<string, StockTicker>
{
    // some stock tickers are 1-10 characters long and can contain letters, numbers, periods 
    private static readonly Regex FormatRegex =
        new("^[a-z0-9.]{1,10}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (FormatRegex.IsMatch(Value)) return;

        var message = $"{Value} is not a valid stock ticket symbol. Only letters, numbers, and a period are allowed. " +
                      $"Allowed length is 1 to 10 characters in total.";
        throw new ValidationException(message, [new ValidationFailure(nameof(StockTicker), message)]);
    }
}