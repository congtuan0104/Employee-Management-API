using Entities.Models;
using Shared.RequestParameters;

namespace Contracts;

public interface IEmployeeRepository
{
    public Task<IEnumerable<Employee>> GetEmployeesAsync(
        Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);

    public Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);

    void CreateEmployeeForCompany(Guid companyId, Employee employee);

    void DeleteEmployee(Employee employee);
}