using Entities;
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

    void UpdateEmployeeForCompany(
        Guid companyId,
        Guid employeeId,
        EmployeeForUpdateDto employeeForUpdate,
        bool companyTrackChanges,
        bool employeeTrackChanges);

    (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(
        Guid companyId,
        Guid employeeId,
        bool companyTrackChanges,
        bool employeeTrackChanges);

    void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}