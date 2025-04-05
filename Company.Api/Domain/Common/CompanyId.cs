using ValueOf;

namespace Company.Api.Domain.Common;

public class CompanyId : ValueOf<Guid, CompanyId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("CompanyId cannot be empty", nameof(CompanyId));
        }
    }
}
