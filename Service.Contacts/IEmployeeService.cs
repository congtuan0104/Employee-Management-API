using Shared.DataTransferObjects;

namespace Service.Contacts;

public interface IEmployeeService
{
    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);

    public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
}