using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters,
        bool trackChanges)
    {
        var employees = await FindByCondition(
                c => c.CompanyId.Equals(companyId), trackChanges)
            .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
            .Search(employeeParameters.SearchTerm)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .Sort(employeeParameters.OrderBy)
            .ToListAsync();

        var count = await FindByCondition(
                c => c.CompanyId.Equals(companyId) &&
                     c.Age >= employeeParameters.MinAge && c.Age <= employeeParameters.MaxAge,
                trackChanges)
            .CountAsync();

        return new PagedList<Employee>(employees, count, employeeParameters.PageNumber,
            employeeParameters.PageSize);
    }

    public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        return await FindByCondition(c => c.CompanyId.Equals(companyId)
                                          && c.Id.Equals(employeeId), trackChanges)
            .SingleOrDefaultAsync();
    }

    public void CreateEmployeeForCompany(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }

    public void DeleteEmployee(Employee employee)
    {
        Delete(employee);
    }
}