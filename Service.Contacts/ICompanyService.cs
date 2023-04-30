using Entities;

namespace Service.Contacts;

public interface ICompanyService
{
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
}