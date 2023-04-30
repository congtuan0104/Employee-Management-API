using Entities;

namespace Contracts;

public interface IEmployeeRepository
{
    public IEnumerable<Employee> GetAllEmployees(bool trackChanges);
}