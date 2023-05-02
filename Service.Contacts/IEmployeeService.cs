using Shared.DataTransferObjects;

namespace Service.Contacts;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);

    EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);

    EmployeeDto CreateEmployeeForCompany(
        Guid companyId,
        EmployeeForCreationDto employeeForCreation,
        bool trackChanges);

    void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges);
}