using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Company>> GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges)
    {
        var companies = await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .Skip(companyParameters.PageSize * (companyParameters.PageNumber - 1))
            .Take(companyParameters.PageSize)
            .ToListAsync();

        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Company>(companies, count, companyParameters.PageNumber, companyParameters.PageSize);
    }

    public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        return await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
    }

    public void CreateCompany(Company company)
    {
        Create(company);
    }

    public void DeleteCompany(Company company)
    {
        Delete(company);
    }
}