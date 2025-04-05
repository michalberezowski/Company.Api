using Company.Api.Contracts.Requests;
using FluentValidation;

namespace Company.Api.Validation;

public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyRequestValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.Exchange).NotEmpty();
        RuleFor(x => x.StockTicker).NotEmpty();
    }
}
