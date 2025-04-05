using Company.Api.Domain.Common.Exceptions;
using Company.Api.Mapping;
using Company.Api.Repositories;
using FluentValidation.Results;

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
        var existingCompany = await GetAsync(company.Id.Value);
        if (existingCompany is not null)
        {
            //Shouldn't happen if we assure unique ids up the call stack (such as Guid.NewGuid()).
            //Added for safety, to differentiate potential error from unique constraint violation on ISIN, below
            var message = $"Cannot create a company with id {company.Id}. Company with that Id already exists";
            throw new UniqueViolationException([new ValidationFailure(nameof(Domain.Company.Id), message)]);
        }

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
