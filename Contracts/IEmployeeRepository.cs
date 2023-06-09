using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IEmployeeRepository
{
    public Task<PagedList<Employee>> GetEmployeesAsync(
        Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);

    public Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);

    void CreateEmployeeForCompany(Guid companyId, Employee employee);

    void DeleteEmployee(Employee employee);
}