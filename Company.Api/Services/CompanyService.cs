using Company.Api.Mapping;
using Company.Api.Repositories;

namespace Company.Api.Services;

public interface ICompanyService
{
    Task<bool> CreateAsync(Domain.Company company);
    Task<Domain.Company?> GetAsync(Guid id);
    Task<IEnumerable<Domain.Company>> GetAllAsync();
    Task<bool> UpdateAsync(Domain.Company company);
    Task<bool> DeleteAsync(Guid id);
    Task<Domain.Company?> GetByIsinAsync(string? isin);
}

public class CompanyService(ICompanyRepository companyRepository) : ICompanyService
{
    public async Task<bool> CreateAsync(Domain.Company company)
    {
        var companyDto = company.ToCompanyDto();
        return await companyRepository.CreateAsync(companyDto);
    }

    public async Task<Domain.Company?> GetAsync(Guid id)
    {
        var companyDto = await companyRepository.GetAsync(id.ToString());
        return companyDto?.ToCompany();
    }
    public async Task<Domain.Company?> GetByIsinAsync(string? id)
    {
        var companyDto = await companyRepository.GetByIsinAsync(id);
        return companyDto?.ToCompany();
    }

    public async Task<IEnumerable<Domain.Company>> GetAllAsync()
    {
        var companyDtos = await companyRepository.GetAllAsync();
        return companyDtos.Select(x => x.ToCompany());
    }

    public async Task<bool> UpdateAsync(Domain.Company company)
    {
        var companyDto = company.ToCompanyDto();
        return await companyRepository.UpdateAsync(companyDto);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await companyRepository.DeleteAsync(id.ToString());
    }
}
