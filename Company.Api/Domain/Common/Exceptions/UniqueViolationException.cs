using FluentValidation;
using FluentValidation.Results;

namespace Company.Api.Domain.Common.Exceptions;

public class UniqueViolationException(IEnumerable<ValidationFailure> errors) : ValidationException(errors);