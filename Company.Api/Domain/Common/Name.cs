using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Company.Api.Domain.Common;

public class Name : ValueOf<string, Name>
{
    //the regex here is for demo purposes only (too restrictive for real world company names). We would have to allow for example
    //unicode characters, national characters, other special chars, etc. Probably best to either find a specialised library, 
    //or accept there will be support calls for "why can't I use my company name" and add them as they come in.
    private static readonly Regex FormatRegex =
        new(@"^[a-z0-9 ,.'\-&@]{1,100}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (FormatRegex.IsMatch(Value)) return;

        var message = $"{Value} is not a valid company name. Only letters, numbers, space and the following symbols are allowed ,.'-&@" +
                      $"Allowed length is 1 to 100 characters";
        throw new ValidationException(message, [new ValidationFailure(nameof(Name), message)]);
    }
}
