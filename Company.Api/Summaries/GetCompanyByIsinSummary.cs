using Company.Api.Contracts.Responses;
using Company.Api.Endpoints;
using FastEndpoints;

namespace Company.Api.Summaries;

public class GetCompanyByIsinSummary : Summary<GetCompanyByIsinEndpoint>
{
    public GetCompanyByIsinSummary()
    {
        Summary = "Returns a single Company by ISIN";
        Description = "Returns a single Company by ISIN";
        Response<CompanyResponse>(200, "Successfully found and returned the Company by ISIN");
        Response(404, "The Company with given ISIN does not exist in the system");
    }
}
