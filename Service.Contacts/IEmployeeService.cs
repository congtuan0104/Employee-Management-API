using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contacts;

public interface IEmployeeService
{
    Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesAsync(
        Guid companyId, LinkParameters linkParameters, bool trackChanges);

    Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);

    Task<EmployeeDto> CreateEmployeeForCompanyAsync(
        Guid companyId,
        EmployeeForCreationDto employeeForCreation,
        bool trackChanges);

    Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges);

    Task UpdateEmployeeForCompanyAsync(
        Guid companyId,
        Guid employeeId,
        EmployeeForUpdateDto employeeForUpdate,
        bool companyTrackChanges,
        bool employeeTrackChanges);

    Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
        Guid companyId,
        Guid employeeId,
        bool companyTrackChanges,
        bool employeeTrackChanges);

    Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
}