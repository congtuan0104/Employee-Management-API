using Shared.DataTransferObjects;

namespace Service.Contacts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
}