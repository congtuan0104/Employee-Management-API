using Entities;

namespace Contracts;

public interface IEmployeeRepository
{
    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);

    public Employee? GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);

    void CreateEmployeeForCompany(Guid companyId, Employee employee);

    void DeleteEmployee(Employee employee);
}