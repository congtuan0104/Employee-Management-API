using Entities.Models;

namespace Contracts;

public interface IEmployeeRepository
{
    public Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges);

    public Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);

    void CreateEmployeeForCompany(Guid companyId, Employee employee);

    void DeleteEmployee(Employee employee);
}