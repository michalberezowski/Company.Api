using Company.Api.Contracts.Requests;
using FluentValidation;

namespace Company.Api.Validation;

public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.Exchange).NotEmpty();
        RuleFor(x => x.StockTicker).NotEmpty();
    }
}
