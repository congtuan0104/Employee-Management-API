using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contacts;

public interface ICompanyService
{
    Task<(IEnumerable<CompanyDto> companies, MetaData metaData)>
        GetAllCompaniesAsync(CompanyParameters companyParameters, bool trackChanges);

    Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);

    Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

    Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);

    Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(
        IEnumerable<CompanyForCreationDto> companyCollection);

    Task DeleteCompanyAsync(Guid companyId, bool trackChanges);

    Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges);
}