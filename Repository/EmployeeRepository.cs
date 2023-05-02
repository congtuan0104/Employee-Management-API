using Contracts;
using Entities;

namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges)
    {
        return FindByCondition(c => c.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(e => e.Name)
            .ToList();
    }

    public Employee? GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
    {
        return FindByCondition(c => c.CompanyId.Equals(companyId)
                                    && c.Id.Equals(employeeId), trackChanges)
            .SingleOrDefault();
    }

    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }
}